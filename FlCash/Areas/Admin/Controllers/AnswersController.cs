using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlCash.Data;

namespace FlCash.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnswersController : Controller
    {
        private readonly DatabaseContext _context;

        public AnswersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Answers
        public async Task<IActionResult> Index(long id)
        {
            ViewData["QuestionId"] = id;
            var databaseContext = _context.Answers.Where(z => z.QuestionId == id).Include(a => a.Question);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/Answers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Admin/Answers/Create
        public IActionResult Create(long id)
        {
            ViewData["QuestionId"] = id;
            return View();
        }

        // POST: Admin/Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,QuestionId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                if(_context.Answers.Where(x => x.QuestionId == answer.QuestionId).Count() >= 4)
                {
                    return RedirectToAction("Index", new { id = answer.QuestionId });
                }
                if(_context.Answers.Where(x => x.QuestionId == answer.QuestionId) != null)
                {
                    var ans = _context.Answers.Where(x => x.QuestionId == answer.QuestionId).ToList();
                    foreach(var item in ans)
                    {
                        if(item.Name == answer.Name)
                        {
                            return RedirectToAction("Index", new { id = answer.QuestionId });
                        }
                    }
                    
                }
                if (_context.Answers.Where(x => x.QuestionId == answer.QuestionId).Count() == 3)
                {
                    var ans = _context.Answers.Where(x => x.QuestionId == answer.QuestionId).ToList();
                    var counter = 0;
                    foreach (var item in ans)
                    {
                        if (item.Status == true)
                        {
                            counter++;
                        }
                    }
                    if(counter == 0)
                    {
                        answer.Status = true;
                    }
                }
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = answer.QuestionId });
            }
            ViewData["QuestionId"] = answer.QuestionId;
            return View(answer);
        }

        // GET: Admin/Answers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = answer.QuestionId;
            return View(answer);
        }

        // POST: Admin/Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Status,QuestionId")] Answer answer)
        {
            if (id != answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = answer.QuestionId });
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", answer.QuestionId);
            return View(answer);
        }

        // GET: Admin/Answers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Admin/Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = answer.QuestionId });
        }

        private bool AnswerExists(long id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}
