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
    public class StoreServicePlansController : Controller
    {
        private readonly DatabaseContext _context;

        public StoreServicePlansController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/StoreServicePlans
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.StoreServicePlans.Include(s => s.StoreService);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/StoreServicePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeServicePlan = await _context.StoreServicePlans
                .Include(s => s.StoreService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeServicePlan == null)
            {
                return NotFound();
            }

            return View(storeServicePlan);
        }

        // GET: Admin/StoreServicePlans/Create
        public IActionResult Create()
        {
            ViewData["StoreServiceId"] = new SelectList(_context.StoreServices, "Id", "Name");
            return View();
        }

        // POST: Admin/StoreServicePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Count,Name,Price,StoreServiceId")] StoreServicePlan storeServicePlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeServicePlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreServiceId"] = new SelectList(_context.StoreServices, "Id", "Name", storeServicePlan.StoreServiceId);
            return View(storeServicePlan);
        }

        // GET: Admin/StoreServicePlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeServicePlan = await _context.StoreServicePlans.FindAsync(id);
            if (storeServicePlan == null)
            {
                return NotFound();
            }
            ViewData["StoreServiceId"] = new SelectList(_context.StoreServices, "Id", "Name", storeServicePlan.StoreServiceId);
            return View(storeServicePlan);
        }

        // POST: Admin/StoreServicePlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Count,Name,Price,StoreServiceId")] StoreServicePlan storeServicePlan)
        {
            if (id != storeServicePlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeServicePlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreServicePlanExists(storeServicePlan.Id))
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
            ViewData["StoreServiceId"] = new SelectList(_context.StoreServices, "Id", "Name", storeServicePlan.StoreServiceId);
            return View(storeServicePlan);
        }

        // GET: Admin/StoreServicePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeServicePlan = await _context.StoreServicePlans
                .Include(s => s.StoreService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeServicePlan == null)
            {
                return NotFound();
            }

            return View(storeServicePlan);
        }

        // POST: Admin/StoreServicePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeServicePlan = await _context.StoreServicePlans.FindAsync(id);
            _context.StoreServicePlans.Remove(storeServicePlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreServicePlanExists(int id)
        {
            return _context.StoreServicePlans.Any(e => e.Id == id);
        }
    }
}
