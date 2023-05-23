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
    public class OffersController : Controller
    {
        private readonly DatabaseContext _context;

        public OffersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Offers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Offers.ToListAsync());
        }

        // GET: Admin/Offers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Offer offer, IFormFile Image)
        {
            if (ModelState.IsValid)
            {

                if (Image != null)
                {
                    var ImageName = DateTime.Now.Hour + DateTime.Now.Second + Image.FileName;
                    var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot", "Uploads",
                            ImageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    offer.Image = $"{Request.Scheme}://{Request.Host}" + "/Uploads/" + ImageName;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Image not valid");
                    return View(offer);
                }
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offer);
        }

        private bool OfferExists(long id)
        {
            return _context.Offers.Any(e => e.Id == id);
        }
    }
}
