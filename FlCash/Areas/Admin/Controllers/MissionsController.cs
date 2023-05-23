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
    public class MissionsController : Controller
    {
        private readonly DatabaseContext _context;

        public MissionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Missions
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Missions.Include(m => m.Category);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/Missions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mission = await _context.Missions
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null)
            {
                return NotFound();
            }

            return View(mission);
        }

        // GET: Admin/Missions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Admin/Missions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuestionCount,CategoryId,Coins,Description")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", mission.CategoryId);
            return View(mission);
        }

        // GET: Admin/Missions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", mission.CategoryId);
            return View(mission);
        }

        // POST: Admin/Missions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestionCount,CategoryId,Coins,Description")] Mission mission)
        {
            if (id != mission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MissionExists(mission.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", mission.CategoryId);
            return View(mission);
        }

        // GET: Admin/Missions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mission = await _context.Missions
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null)
            {
                return NotFound();
            }

            return View(mission);
        }

        // POST: Admin/Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MissionExists(int id)
        {
            return _context.Missions.Any(e => e.Id == id);
        }
    }
}
