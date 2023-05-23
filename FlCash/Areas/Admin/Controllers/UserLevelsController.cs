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
    public class UserLevelsController : Controller
    {
        private readonly DatabaseContext _context;

        public UserLevelsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/UserLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserLevels.ToListAsync());
        }

        // GET: Admin/UserLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLevel = await _context.UserLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLevel == null)
            {
                return NotFound();
            }

            return View(userLevel);
        }

        // GET: Admin/UserLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuestionCount,Name")] UserLevel userLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userLevel);
        }

        // GET: Admin/UserLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLevel = await _context.UserLevels.FindAsync(id);
            if (userLevel == null)
            {
                return NotFound();
            }
            return View(userLevel);
        }

        // POST: Admin/UserLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionCount,Name")] UserLevel userLevel)
        {
            if (id != userLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLevelExists(userLevel.Id))
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
            return View(userLevel);
        }

        // GET: Admin/UserLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLevel = await _context.UserLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLevel == null)
            {
                return NotFound();
            }

            return View(userLevel);
        }

        // POST: Admin/UserLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userLevel = await _context.UserLevels.FindAsync(id);
            _context.UserLevels.Remove(userLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLevelExists(int id)
        {
            return _context.UserLevels.Any(e => e.Id == id);
        }
    }
}
