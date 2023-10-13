using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QA_Feedback.Models;

namespace QA_Feedback.Controllers
{
    public class RatingController : Controller
    {
        private readonly QuizContext _context;

        public RatingController(QuizContext context)
        {
            _context = context;
        }

        // GET: Rating
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            return _context.Rating != null ? 
                          View(await _context.Rating.ToListAsync()) :
                          Problem("Entity set 'QuizContext.Rating'  is null.");
        }

        // GET: Rating/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Rating/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rating/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,User,Description,Difficulty_Stars,Pyramidality_Stars,Accuracy_Stars")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rating);
        }

        // GET: Rating/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return View(rating);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,User,Description,Difficulty_Stars,Pyramidality_Stars,Accuracy_Stars")] Rating rating)
        {
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
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
            return View(rating);
        }

        // GET: Rating/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            if (id == null || _context.Rating == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rating == null)
            {
                return Problem("Entity set 'QuizContext.Rating'  is null.");
            }
            var rating = await _context.Rating.FindAsync(id);
            if (rating != null)
            {
                _context.Rating.Remove(rating);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
          return (_context.Rating?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
