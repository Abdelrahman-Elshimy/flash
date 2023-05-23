using FlCash.Data;
using FlCash.DTOs;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgesController : Controller
    {
        private readonly DatabaseContext _context;

        public BadgesController(DatabaseContext context)
        {
            _context = context;
        }
        // Get all categories with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Badges")]
        public IActionResult Badges([FromBody] UserIdModel userIdModel)
        {
            try
            {
                var user = _context.Users.Find(userIdModel.userId);
                var badgesAll = _context.Badges.ToList();
                if (user != null)
                {
                    var firstLogin = badgesAll.Select( x => new badgeModelDto
                    {
                        Description = x.Description,
                        Logo = x.Logo,
                        Name = x.Name
                    }).First();
                    List<badgeModelDto> badges = new List<badgeModelDto>
                    {
                        firstLogin
                    };
                    if (user.PlayedGamesCounter > 50)
                    {
                        var ro2an = badgesAll.FirstOrDefault(x => x.Id == 2);
                        var y = new badgeModelDto
                        {
                            Description = ro2an.Description,
                            Logo = ro2an.Logo,
                            Name = ro2an.Name
                        };
                        badges.Add(y);

                    }

                    if (user.RightCounterCounter > 50)
                    {
                        var ro2an = badgesAll.FirstOrDefault(x => x.Id == 6);
                        var y = new badgeModelDto
                        {
                            Description = ro2an.Description,
                            Logo = ro2an.Logo,
                            Name = ro2an.Name
                        };
                        badges.Add(y);

                    }
                    if (user.RightCounterCounter > 150)
                    {
                        var ro2an = badgesAll.FirstOrDefault(x => x.Id == 7);
                        var y = new badgeModelDto
                        {
                            Description = ro2an.Description,
                            Logo = ro2an.Logo,
                            Name = ro2an.Name
                        };
                        badges.Add(y);

                    }

                    return Ok(new { Badges = badges});

                }
                else
                {
                    var error = new Error
                    {
                        Message = "Failed operation",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get achievements",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
