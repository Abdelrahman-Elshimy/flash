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
    public class TriviaGiftsController : Controller
    {
        private readonly DatabaseContext _context;

        public TriviaGiftsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/TriviaGifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TriviaGift.ToListAsync());
        }

        // GET: Admin/TriviaGifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var triviaGift = await _context.TriviaGift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (triviaGift == null)
            {
                return NotFound();
            }

            return View(triviaGift);
        }

        // GET: Admin/TriviaGifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TriviaGifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Logo,CorrectAnswers,hearts,Coins,Tickets")] TriviaGift triviaGift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(triviaGift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(triviaGift);
        }

        // GET: Admin/TriviaGifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var triviaGift = await _context.TriviaGift.FindAsync(id);
            if (triviaGift == null)
            {
                return NotFound();
            }
            return View(triviaGift);
        }

        // POST: Admin/TriviaGifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,CorrectAnswers,hearts,Coins,Tickets")] TriviaGift triviaGift)
        {
            if (id != triviaGift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(triviaGift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TriviaGiftExists(triviaGift.Id))
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
            return View(triviaGift);
        }

        // GET: Admin/TriviaGifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var triviaGift = await _context.TriviaGift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (triviaGift == null)
            {
                return NotFound();
            }

            return View(triviaGift);
        }

        // POST: Admin/TriviaGifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var triviaGift = await _context.TriviaGift.FindAsync(id);
            _context.TriviaGift.Remove(triviaGift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TriviaGiftExists(int id)
        {
            return _context.TriviaGift.Any(e => e.Id == id);
        }
    }
}
