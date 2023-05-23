using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlCash.Data;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SettingsController(DatabaseContext context)
        {
            _context = context;
        }
        // Get all categories with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            try
            {
                var settings = _context.Settings.First();
                return Ok(new { Message = settings.Privacy });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get privacy",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
        // Get all categories with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Terms")]
        public IActionResult Terms()
        {
            try
            {
                var settings = _context.Settings.First();
                return Ok(new { Message = settings.Terms });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get Terms",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
        // Get all categories with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("AboutUs")]
        public IActionResult AboutUs()
        {
            try
            {
                var settings = _context.Settings.First();
                return Ok(new { Message = settings.AboutUs });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get Trivia Gifts",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}

