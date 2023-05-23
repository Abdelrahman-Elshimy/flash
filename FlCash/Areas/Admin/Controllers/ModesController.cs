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
    public class ModesController : Controller
    {
        private readonly DatabaseContext _context;

        public ModesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Modes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Modes.ToListAsync());
        }

        // GET: Admin/Modes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mode = await _context.Modes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mode == null)
            {
                return NotFound();
            }

            return View(mode);
        }

        // GET: Admin/Modes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Modes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mode mode, IFormFile Image)
        {
            if (ModelState.IsValid)
            {

                if (Image != null)
                {
                    var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Image.FileName;
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Modes",
                            ImageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    mode.Image = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/Modes/" + ImageName;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Image not valid");
                    return View(mode);
                }
                _context.Add(mode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mode);
        }

        // GET: Admin/Modes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mode = await _context.Modes.FindAsync(id);
            if (mode == null)
            {
                return NotFound();
            }
            return View(mode);
        }

        // POST: Admin/Modes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Mode mode, IFormFile Image)
        {
            if (id != mode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null)
                    {
                        var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Image.FileName;
                        var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Modes",
                                ImageName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }
                        mode.Image = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/Modes/" + ImageName;
                    }
                    _context.Update(mode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeExists(mode.Id))
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
            return View(mode);
        }

        // GET: Admin/Modes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mode = await _context.Modes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mode == null)
            {
                return NotFound();
            }

            return View(mode);
        }

        // POST: Admin/Modes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var mode = await _context.Modes.FindAsync(id);
            _context.Modes.Remove(mode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeExists(long id)
        {
            return _context.Modes.Any(e => e.Id == id);
        }
    }
}
