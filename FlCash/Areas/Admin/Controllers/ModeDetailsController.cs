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
    public class ModeDetailsController : Controller
    {
        private readonly DatabaseContext _context;

        public ModeDetailsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/ModeDetails
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ModeDetail.Include(m => m.Mode);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/ModeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeDetail = await _context.ModeDetail
                .Include(m => m.Mode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeDetail == null)
            {
                return NotFound();
            }

            return View(modeDetail);
        }

        // GET: Admin/ModeDetails/Create
        public IActionResult Create()
        {
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name");
            return View();
        }

        // POST: Admin/ModeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Start,ModeId")] ModeDetail modeDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modeDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", modeDetail.ModeId);
            return View(modeDetail);
        }

        // GET: Admin/ModeDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeDetail = await _context.ModeDetail.FindAsync(id);
            if (modeDetail == null)
            {
                return NotFound();
            }
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", modeDetail.ModeId);
            return View(modeDetail);
        }

        // POST: Admin/ModeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Start,ModeId")] ModeDetail modeDetail)
        {
            if (id != modeDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modeDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeDetailExists(modeDetail.Id))
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
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", modeDetail.ModeId);
            return View(modeDetail);
        }

        // GET: Admin/ModeDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeDetail = await _context.ModeDetail
                .Include(m => m.Mode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeDetail == null)
            {
                return NotFound();
            }

            return View(modeDetail);
        }

        // POST: Admin/ModeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modeDetail = await _context.ModeDetail.FindAsync(id);
            _context.ModeDetail.Remove(modeDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeDetailExists(int id)
        {
            return _context.ModeDetail.Any(e => e.Id == id);
        }
    }
}
