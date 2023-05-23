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
    public class ElmarkaGiftsController : Controller
    {
        private readonly DatabaseContext _context;

        public ElmarkaGiftsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/ElmarkaGifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.ElmarkaGifts.ToListAsync());
        }

        // GET: Admin/ElmarkaGifts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elmarkaGift = await _context.ElmarkaGifts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (elmarkaGift == null)
            {
                return NotFound();
            }

            return View(elmarkaGift);
        }

        // GET: Admin/ElmarkaGifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ElmarkaGifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Coins")] ElmarkaGift elmarkaGift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(elmarkaGift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(elmarkaGift);
        }

        // GET: Admin/ElmarkaGifts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elmarkaGift = await _context.ElmarkaGifts.FindAsync(id);
            if (elmarkaGift == null)
            {
                return NotFound();
            }
            return View(elmarkaGift);
        }

        // POST: Admin/ElmarkaGifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Coins")] ElmarkaGift elmarkaGift)
        {
            if (id != elmarkaGift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elmarkaGift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElmarkaGiftExists(elmarkaGift.Id))
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
            return View(elmarkaGift);
        }

        // GET: Admin/ElmarkaGifts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elmarkaGift = await _context.ElmarkaGifts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (elmarkaGift == null)
            {
                return NotFound();
            }

            return View(elmarkaGift);
        }

        // POST: Admin/ElmarkaGifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var elmarkaGift = await _context.ElmarkaGifts.FindAsync(id);
            _context.ElmarkaGifts.Remove(elmarkaGift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElmarkaGiftExists(long id)
        {
            return _context.ElmarkaGifts.Any(e => e.Id == id);
        }
    }
}
