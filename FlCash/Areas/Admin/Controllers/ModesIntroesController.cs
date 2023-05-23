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
    public class ModesIntroesController : Controller
    {
        private readonly DatabaseContext _context;

        public ModesIntroesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/ModesIntroes
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ModesIntros.Include(m => m.Mode);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/ModesIntroes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modesIntro = await _context.ModesIntros
                .Include(m => m.Mode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modesIntro == null)
            {
                return NotFound();
            }

            return View(modesIntro);
        }

        // GET: Admin/ModesIntroes/Create
        public IActionResult Create()
        {
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name");
            return View();
        }

        // POST: Admin/ModesIntroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModesIntro modesIntro, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Image.FileName;
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "ModesIntros",
                            ImageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    modesIntro.Image = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/ModesIntros/" + ImageName;
                }
                else
                {
                    ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", modesIntro.ModeId);
                    ModelState.AddModelError(string.Empty, "Image not valid");
                    return View(modesIntro);
                }
                _context.Add(modesIntro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", modesIntro.ModeId);
            return View(modesIntro);
        }

        // GET: Admin/ModesIntroes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modesIntro = await _context.ModesIntros.FindAsync(id);
            if (modesIntro == null)
            {
                return NotFound();
            }
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", modesIntro.ModeId);
            return View(modesIntro);
        }

        // POST: Admin/ModesIntroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ModesIntro modesIntro, IFormFile Image)
        {
            if (id != modesIntro.Id)
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
                                Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "ModesIntros",
                                ImageName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }
                        modesIntro.Image = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/ModesIntros/" + ImageName;
                        _context.Update(modesIntro);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Image not valid");
                        return View(modesIntro);
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModesIntroExists(modesIntro.Id))
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
            ViewData["ModeId"] = new SelectList(_context.Modes, "Id", "Name", modesIntro.ModeId);
            return View(modesIntro);
        }

        // GET: Admin/ModesIntroes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modesIntro = await _context.ModesIntros
                .Include(m => m.Mode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modesIntro == null)
            {
                return NotFound();
            }

            return View(modesIntro);
        }

        // POST: Admin/ModesIntroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var modesIntro = await _context.ModesIntros.FindAsync(id);
            _context.ModesIntros.Remove(modesIntro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModesIntroExists(long id)
        {
            return _context.ModesIntros.Any(e => e.Id == id);
        }
    }
}
