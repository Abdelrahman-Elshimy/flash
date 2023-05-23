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
    public class AchievementsController : Controller
    {
        private readonly DatabaseContext _context;

        public AchievementsController(DatabaseContext context)
        {
            _context = context;
        }
                                                                                                                                                                                      
        // GET: Admin/Achievements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Achievements.ToListAsync());
        }

        // GET: Admin/Achievements/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achievement == null)
            {
                return NotFound();
            }

            return View(achievement);
        }

        // GET: Admin/Achievements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Achievements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Achievement achievement, IFormFile Logo)
        {

            if (ModelState.IsValid)
            {
                if (Logo != null)
                {
                    var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Logo.FileName;
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Achievements",
                            ImageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Logo.CopyToAsync(stream);
                    }
                    achievement.Logo = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/Achievements/" + ImageName;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Image not valid");
                    return View(achievement);
                }
                _context.Add(achievement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(achievement);
        }

        // GET: Admin/Achievements/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements.FindAsync(id);
            if (achievement == null)
            {
                return NotFound();
            }
            return View(achievement);
        }

        // POST: Admin/Achievements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Achievement achievement, IFormFile Logo)
        {
            if (id != achievement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo != null)
                    {
                        var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Logo.FileName;
                        var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Achievements",
                                ImageName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await Logo.CopyToAsync(stream);
                        }
                        achievement.Logo = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/Achievements/" + ImageName;
                        _context.Update(achievement);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Image not valid");
                        return View(achievement);
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchievementExists(achievement.Id))
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
            return View(achievement);
        }

        // GET: Admin/Achievements/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achievement == null)
            {
                return NotFound();
            }

            return View(achievement);
        }

        // POST: Admin/Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var achievement = await _context.Achievements.FindAsync(id);
            _context.Achievements.Remove(achievement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchievementExists(long id)
        {
            return _context.Achievements.Any(e => e.Id == id);
        }
    }
}
