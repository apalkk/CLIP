
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using QA_Feedback.Models;

namespace QA_Feedback.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly QuizContext _context;

        public QuestionsController(QuizContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            var stream = file.OpenReadStream();

            try
            {
                using (TextFieldParser reader = new TextFieldParser(stream))
                {
                    reader.TextFieldType = FieldType.Delimited;
                    reader.SetDelimiters(",");

                    int count = 0;
                    // id,topic,method,question,difficulty,subdifficulty,tournament

                    List<string> id = new();
                    List<string> topic = new();
                    List<string> method = new();
                    List<string> question = new();
                    List<string> difficulty = new();
                    List<string> subdifficulty = new();
                    List<string> tournament = new();

                    reader.ReadLine();

                    while (!reader.EndOfData)
                    {
                        string[] values = reader.ReadFields();

                        id.Add(values[0]);
                        topic.Add(values[1]);
                        method.Add(values[2]);
                        question.Add(values[3]);
                        difficulty.Add(values[4]);
                        subdifficulty.Add(values[5]);
                        tournament.Add(values[6]);
                        count++;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Source s = new();
                        Question q = new();

                        s.Question = int.Parse(id[i]);
                        s.Title = topic[i];
                        s.Id = s.Question;
                        if (!_context.Source.Any(s => s.Title == topic[i]))
                        {
                            _context.Source.Add(s);
                        }

                        q.Method = method[i];
                        q.QuestionText = question[i];
                        q.Source = int.Parse(id[i]);
                        _context.Question.Add(q);
                        _context.SaveChanges();
                    }
                }
            }
            catch
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            return View("~/Views/Home/Landing.cshtml");

        }

        public async Task<IActionResult> UploadFileX()
        {
            _context.SaveChanges();
            return View();
        }

        public async Task<IActionResult> DeleteSuper()
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }
            DeleteAll();
            return View("~/Views/Home/Landing.cshtml");
        }

        public async void DeleteAll()
        {
            foreach (var item in _context.Question)
            {
                _context.Question.Remove(item);
            }
            foreach (var item in _context.Rating)
            {
                _context.Rating.Remove(item);
            }
            foreach (var item in _context.Source)
            {
                _context.Source.Remove(item);
            }
        }

            // GET: Questions
            public async Task<IActionResult> Index()
            {
                if (HttpContext.Session.GetString("_Auth") == "False")
                {
                    return View("~/Views/Home/Landing.cshtml");
                }

                return _context.Question != null ?
                          View(await _context.Question.ToListAsync()) :
                          Problem("Entity set 'QuizContext.Question'  is null.");
            }

            // GET: Questions/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (HttpContext.Session.GetString("_Auth") == "False")
                {
                    return View("~/Views/Home/Landing.cshtml");
                }

                if (id == null || _context.Question == null)
                {
                    return NotFound();
                }

                var question = await _context.Question
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (question == null)
                {
                    return NotFound();
                }

                return View(question);
            }

            // GET: Questions/Create
            public IActionResult Create()
            {
                return View();
            }

            public IActionResult GoTo()
            {
                return View();
            }


            // POST: Questions/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,QuestionText,Method,Source")] Question question)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(question);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(question);
            }

            // GET: Questions/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (HttpContext.Session.GetString("_Auth") == "False")
                {
                    return View("~/Views/Home/Landing.cshtml");
                }

                if (id == null || _context.Question == null)
                {
                    return NotFound();
                }

                var question = await _context.Question.FindAsync(id);
                if (question == null)
                {
                    return NotFound();
                }
                return View(question);
            }

            // POST: Questions/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionText,Method,Source")] Question question)
            {
                if (id != question.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(question);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!QuestionExists(question.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(question);
            }

            // GET: Questions/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (HttpContext.Session.GetString("_Auth") == "False")
                {
                    return View("~/Views/Home/Landing.cshtml");
                }

                if (id == null || _context.Question == null)
                {
                    return NotFound();
                }

                var question = await _context.Question
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (question == null)
                {
                    return NotFound();
                }

                return View(question);
            }

            // POST: Questions/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Question == null)
                {
                    return Problem("Entity set 'QuizContext.Question'  is null.");
                }
                var question = await _context.Question.FindAsync(id);
                if (question != null)
                {
                    _context.Question.Remove(question);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool QuestionExists(int id)
            {
                return (_context.Question?.Any(e => e.Id == id)).GetValueOrDefault();
            }

            [HttpGet]
            public async Task<IActionResult> Jump(string Source)
            {
                return Redirect($"/Questions/ask/{Source}");
            }


            [HttpGet]
            public async Task<IActionResult> Ask(int id)
            {
                var x = _context.Question.Where<Question>(q => q.Source == id).ToList();
                try
                {
                    var src = _context.Source.Where<Source>(q => q.Id == id).ToList().First();
                    ViewData["title"] = src.Title;

                }
                catch (Exception e)
                {
                    ViewData["title"] = "Not Found";
                }


                Random rnd = new Random();
                int random = rnd.Next(4);

                ViewData["source"] = id;


                try
                {
                    if (random == 2)
                    {
                        ViewData["1"] = x[0].QuestionText;
                        ViewData["2"] = x[1].QuestionText;
                        ViewData["3"] = x[2].QuestionText;
                        ViewData["1i"] = x[0].Id;
                        ViewData["2i"] = x[1].Id;
                        ViewData["3i"] = x[2].Id;
                        ViewData["id"] = id;
                        ViewData["next"] = id + 1;
                    }
                    else if (random == 1)
                    {
                        ViewData["1"] = x[1].QuestionText;
                        ViewData["2"] = x[0].QuestionText;
                        ViewData["3"] = x[2].QuestionText;
                        ViewData["1i"] = x[1].Id;
                        ViewData["2i"] = x[0].Id;
                        ViewData["3i"] = x[2].Id;
                        ViewData["id"] = id;
                        ViewData["next"] = id + 1;

                    }
                    else if (random == 1)
                    {
                        ViewData["1"] = x[1].QuestionText;
                        ViewData["2"] = x[2].QuestionText;
                        ViewData["3"] = x[0].QuestionText;
                        ViewData["1i"] = x[1].Id;
                        ViewData["2i"] = x[2].Id;
                        ViewData["3i"] = x[0].Id;
                        ViewData["id"] = id;
                        ViewData["next"] = id + 1;

                    }

                    else
                    {
                        ViewData["1"] = x[2].QuestionText;
                        ViewData["2"] = x[0].QuestionText;
                        ViewData["3"] = x[1].QuestionText;
                        ViewData["1i"] = x[2].Id;
                        ViewData["2i"] = x[0].Id;
                        ViewData["3i"] = x[1].Id;
                        ViewData["id"] = id;
                        ViewData["next"] = id + 1;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return View("~/Views/Home/Incorrect.cshtml");
                }

                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Set(int id1, int id2, int id3, int Pyramidality_Stars1, int Pyramidality_Stars2, int Pyramidality_Stars3, int Difficulty_Stars1, int Difficulty_Stars2, int Difficulty_Stars3, int Accuracy_Stars1, int Accuracy_Stars2, int Accuracy_Stars3, string d1, string d2, string d3, int next)
            {
                if (d1 == null || d2 == null || d3 == null || Pyramidality_Stars1 == 0 || Pyramidality_Stars2 == 0 || Pyramidality_Stars3 == 0 || Difficulty_Stars1 == 0 || Difficulty_Stars2 == 0 || Difficulty_Stars3 == 0 || Accuracy_Stars1 == 0 || Accuracy_Stars2 == 0 || Accuracy_Stars3 == 0)
                {
                    return Redirect($"/Questions/ask/{next - 1}");
                }

                var x = _context.Question.Where(s => s.Id == id1).First();
                var y = _context.Question.Where(s => s.Id == id2).First();
                var z = _context.Question.Where(s => s.Id == id3).First();
                Rating r = new();
                r.Description = d1;
                r.Question = id1;
                r.Pyramidality_Stars = Pyramidality_Stars1;
                r.Difficulty_Stars = Difficulty_Stars1;
                r.Accuracy_Stars = Accuracy_Stars1;
                r.User = HttpContext.Session.GetString("_Name") ?? "";
                _context.Rating.Add(r);
                await _context.SaveChangesAsync();

                Rating r2 = new();
                r2.Description = d2;
                r2.Question = id2;
                r2.Pyramidality_Stars = Pyramidality_Stars2;
                r2.Difficulty_Stars = Difficulty_Stars2;
                r2.Accuracy_Stars = Accuracy_Stars2;
                r2.User = HttpContext.Session.GetString("_Name") ?? "";
                _context.Rating.Add(r2);
                await _context.SaveChangesAsync();

                Rating r3 = new();
                r3.Description = d3;
                r3.Question = id3;
                r3.Pyramidality_Stars = Pyramidality_Stars3;
                r3.Difficulty_Stars = Difficulty_Stars3;
                r3.Accuracy_Stars = Accuracy_Stars3;
                r3.User = HttpContext.Session.GetString("_Name") ?? "";
                _context.Rating.Add(r3);
                await _context.SaveChangesAsync();

                return Redirect($"/Questions/ask/{next}");
            }

        }
    }
