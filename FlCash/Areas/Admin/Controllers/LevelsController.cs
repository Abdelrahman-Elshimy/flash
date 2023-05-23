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
    public class LevelsController : Controller
    {
        private readonly DatabaseContext _context;

        public LevelsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Levels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Levels.ToListAsync());
        }

        // GET: Admin/Levels/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var level = await _context.Levels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }

        // GET: Admin/Levels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Level level)
        {
            if (ModelState.IsValid)
            {
                _context.Add(level);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(level);
        }

        // GET: Admin/Levels/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var level = await _context.Levels.FindAsync(id);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        // POST: Admin/Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Level level)
        {
            if (id != level.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(level);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LevelExists(level.Id))
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
            return View(level);
        }

        // GET: Admin/Levels/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var level = await _context.Levels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }

        // POST: Admin/Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var level = await _context.Levels.FindAsync(id);
            _context.Levels.Remove(level);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LevelExists(long id)
        {
            return _context.Levels.Any(e => e.Id == id);
        }
    }
}
