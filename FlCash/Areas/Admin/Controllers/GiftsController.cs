using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlCash.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FlCash.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiftsController : Controller
    {
        private readonly DatabaseContext _context;

        public GiftsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Gifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gift.ToListAsync());
        }

        // GET: Admin/Gifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // GET: Admin/Gifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Gifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gift gift, IFormFile Logo)
        {
            if (ModelState.IsValid)
            {
                if (Logo != null)
                {
                    var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Logo.FileName;
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Gifts",
                            ImageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Logo.CopyToAsync(stream);
                    }
                    gift.Logo = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/Gifts/" + ImageName;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Image not valid");
                    return View(gift);
                }
                _context.Add(gift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }

        // GET: Admin/Gifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gift.FindAsync(id);
            if (gift == null)
            {
                return NotFound();
            }
            return View(gift);
        }

        // POST: Admin/Gifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Coins,Tickets,CorrectAnswers")] Gift gift)
        {
            if (id != gift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftExists(gift.Id))
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
            return View(gift);
        }

        // GET: Admin/Gifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // POST: Admin/Gifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gift = await _context.Gift.FindAsync(id);
            _context.Gift.Remove(gift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftExists(int id)
        {
            return _context.Gift.Any(e => e.Id == id);
        }
    }
}
