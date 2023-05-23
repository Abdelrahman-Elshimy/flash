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
    public class DailyGiftsController : Controller
    {
        private readonly DatabaseContext _context;

        public DailyGiftsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/DailyGifts
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.DailyGifts.Include(d => d.StoreService);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/DailyGifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyGift = await _context.DailyGifts
                .Include(d => d.StoreService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyGift == null)
            {
                return NotFound();
            }

            return View(dailyGift);
        }

        // GET: Admin/DailyGifts/Create
        public IActionResult Create()
        {
            ViewData["StoreServiceId"] = new SelectList(_context.StoreServices, "Id", "Name");
            return View();
        }

        // POST: Admin/DailyGifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,Image,Value,StoreServiceId")] DailyGift dailyGift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyGift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreServiceId"] = new SelectList(_context.StoreServices, "Id", "Name", dailyGift.StoreServiceId);
            return View(dailyGift);
        }

        // GET: Admin/DailyGifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyGift = await _context.DailyGifts.FindAsync(id);
            if (dailyGift == null)
            {
                return NotFound();
            }
            ViewData["StoreServiceId"] = new SelectList(_context.StoreServices, "Id", "Name", dailyGift.StoreServiceId);
            return View(dailyGift);
        }

        // POST: Admin/DailyGifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,Image,Value,StoreServiceId")] DailyGift dailyGift)
        {
            if (id != dailyGift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyGift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyGiftExists(dailyGift.Id))
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
            ViewData["StoreServiceId"] = new SelectList(_context.StoreServices, "Id", "Name", dailyGift.StoreServiceId);
            return View(dailyGift);
        }

        // GET: Admin/DailyGifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyGift = await _context.DailyGifts
                .Include(d => d.StoreService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyGift == null)
            {
                return NotFound();
            }

            return View(dailyGift);
        }

        // POST: Admin/DailyGifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyGift = await _context.DailyGifts.FindAsync(id);
            _context.DailyGifts.Remove(dailyGift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyGiftExists(int id)
        {
            return _context.DailyGifts.Any(e => e.Id == id);
        }
    }
}
