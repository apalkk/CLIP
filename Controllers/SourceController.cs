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
    public class SourceController : Controller
    {
        private readonly QuizContext _context;

        public SourceController(QuizContext context)
        {
            _context = context;
        }

        // GET: Source
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            return _context.Source != null ? 
                          View(await _context.Source.ToListAsync()) :
                          Problem("Entity set 'QuizContext.Source'  is null.");
        }

        // GET: Source/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            if (id == null || _context.Source == null)
            {
                return NotFound();
            }

            var source = await _context.Source
                .FirstOrDefaultAsync(m => m.Id == id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // GET: Source/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            return View();
        }

        // POST: Source/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Stars,Question,Title")] Source source)
        {
            if (ModelState.IsValid)
            {
                _context.Add(source);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(source);
        }

        // GET: Source/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Source == null)
            {
                return NotFound();
            }

            var source = await _context.Source.FindAsync(id);
            if (source == null)
            {
                return NotFound();
            }
            return View(source);
        }

        // POST: Source/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Stars,Question,Title")] Source source)
        {

            if (id != source.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(source);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SourceExists(source.Id))
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
            return View(source);
        }

        // GET: Source/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("_Auth") == "False")
            {
                return View("~/Views/Home/Landing.cshtml");
            }

            if (id == null || _context.Source == null)
            {
                return NotFound();
            }

            var source = await _context.Source
                .FirstOrDefaultAsync(m => m.Id == id);
            if (source == null)
            {
                return NotFound();
            }

            return View(source);
        }

        // POST: Source/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Source == null)
            {
                return Problem("Entity set 'QuizContext.Source'  is null.");
            }
            var source = await _context.Source.FindAsync(id);
            if (source != null)
            {
                _context.Source.Remove(source);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SourceExists(int id)
        {
          return (_context.Source?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
