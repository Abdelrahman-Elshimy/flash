using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlCash.Authentication;
using FlCash.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace FlCash.Util
{
    public class UtilController : Controller
    {
        private readonly DatabaseContext _context;
        public UtilController(DatabaseContext context)
        {
            _context = context;
        }
        public UtilController() { }
        public IActionResult Index()
        {
            return View();
        }

        public void ResetPointsWeekly()
        {
            var users = _context.Users.ToList();
            users.ForEach(user =>
            {
                user.PointsInWeek = 0;
                _context.SaveChanges();
            });
        }

        public void ResetPointsMonthly()
        {
            var users = _context.Users.ToList();
            users.ForEach(user =>
            {
                user.PointsInMonth = 0;
                _context.SaveChanges();
            });
        }

    }
}
