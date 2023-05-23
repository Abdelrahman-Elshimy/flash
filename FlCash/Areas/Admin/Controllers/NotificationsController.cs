using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlCash.Data;
using FlCash.Controllers;

namespace FlCash.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationsController : Controller
    {
        private readonly DatabaseContext _context;

        public NotificationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Notifications
        public async Task<IActionResult> Index()
        {

            if (TempData.ContainsKey("successNotification"))
                ViewBag.successNotifications = TempData["successNotification"].ToString();
            if (TempData.ContainsKey("errorNotification"))
                ViewBag.errorNotifications = TempData["errorNotification"].ToString();

            return View(await _context.Notifications.ToListAsync());
        }


        // GET: Admin/Notifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notification notification)
        {
            if (ModelState.IsValid)
            {
                notification.DateCreated = DateTime.Now;
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notification);
        }
        [HttpGet]
        public IActionResult SendNotification(int id)
        {
            FCMPushNotification fCMPush = new FCMPushNotification();
            var notification = _context.Notifications.Find(id);
            if(notification != null)
            {
                var x = fCMPush.SendNotification(notification.Title, notification.Body, "flash");
                TempData["successNotification"] = "Notification Sent";

                return RedirectToAction("Index");
            }else
            {
                TempData["errorNotification"] = "Notification Not Sent";
                return RedirectToAction("Index");
            }
            


        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
