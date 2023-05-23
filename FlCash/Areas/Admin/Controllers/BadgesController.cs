using FlCash.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace FlCash.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BadgesController : Controller
    {
        private readonly DatabaseContext _context;

        public BadgesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Achievements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Badges.ToListAsync());
        }

        // GET: Admin/Achievements/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Badges
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
        public async Task<IActionResult> Create(Badge achievement, IFormFile Logo, IFormFile InitialLogo)
        {

            if (ModelState.IsValid)
            {
                if (Logo != null && InitialLogo != null)
                {
                    var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Logo.FileName;
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Badges",
                            ImageName);
                    var initlalImageName = DateTime.Now.Hour + DateTime.Now.Second + InitialLogo.FileName;
                    var initlalpath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Badges",
                            initlalImageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Logo.CopyToAsync(stream);
                    }
                    using (var stream = new FileStream(initlalpath, FileMode.Create))
                    {
                        await InitialLogo.CopyToAsync(stream);
                    }
                    achievement.Logo = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/Badges/" + ImageName;
                    achievement.InitialLogo = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/Badges/" + initlalImageName;
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

       
        
        // GET: Admin/Achievements/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Badges
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
            var achievement = await _context.Badges.FindAsync(id);
            _context.Badges.Remove(achievement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchievementExists(long id)
        {
            return _context.Badges.Any(e => e.Id == id);
        }
    }
}
