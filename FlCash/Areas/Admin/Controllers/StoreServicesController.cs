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
    public class StoreServicesController : Controller
    {
        private readonly DatabaseContext _context;

        public StoreServicesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/StoreServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.StoreServices.ToListAsync());
        }

        // GET: Admin/StoreServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeService = await _context.StoreServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeService == null)
            {
                return NotFound();
            }

            return View(storeService);
        }

        // GET: Admin/StoreServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/StoreServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreService storeService, IFormFile Logo)
        {
            if (ModelState.IsValid)
            {

                if (Logo != null)
                {
                    var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Logo.FileName;
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "StoreServices",
                            ImageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Logo.CopyToAsync(stream);
                    }
                    storeService.Logo = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/StoreServices/" + ImageName;
                }
                _context.Add(storeService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeService);
        }

        // GET: Admin/StoreServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeService = await _context.StoreServices.FindAsync(id);
            if (storeService == null)
            {
                return NotFound();
            }
            return View(storeService);
        }

        // POST: Admin/StoreServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreService storeService, IFormFile Logo)
        {
            if (id != storeService.Id)
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
                                Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "StoreServices",
                                ImageName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await Logo.CopyToAsync(stream);
                        }
                        storeService.Logo = $"{Request.Scheme}://{Request.Host}" + "/Uploads" + "/StoreServices/" + ImageName;
                    }
                    _context.Update(storeService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreServiceExists(storeService.Id))
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
            return View(storeService);
        }

        // GET: Admin/StoreServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeService = await _context.StoreServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeService == null)
            {
                return NotFound();
            }

            return View(storeService);
        }

        // POST: Admin/StoreServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeService = await _context.StoreServices.FindAsync(id);
            _context.StoreServices.Remove(storeService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreServiceExists(int id)
        {
            return _context.StoreServices.Any(e => e.Id == id);
        }
    }
}
