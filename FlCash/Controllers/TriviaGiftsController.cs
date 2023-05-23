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
    public class TriviaGiftsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TriviaGiftsController(DatabaseContext context)
        {
            _context = context;
        }
        // Get all categories with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetTriviaGifts")]
        public IActionResult GetTriviaGifts([FromBody] UserIdModel userIdModel)
        {
            try
            {
                var trivi = _context.Trivias.ToList();
                string date = "";
                bool Status = false;
                var isPlayed = _context.UserTriviaGifteds.FirstOrDefault(x => x.UserId == userIdModel.userId);

                if(isPlayed != null)
                {
                    Status = true;
                }else
                {
                    Status = false;
                }
                
                if(trivi.Count <= 0)
                {
                    var t = new Trivia
                    {
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(1) 
                    };
                    _context.Trivias.Add(t);
                    _context.SaveChanges();
                }
                var tr = _context.Trivias.First();
                if(DateTime.Now > tr.EndDate) 
                {
                    tr.StartDate = DateTime.Now;
                    tr.EndDate = DateTime.Now.AddDays(1);

                    _context.SaveChanges();
                    var ugh = _context.UserTriviaGifteds.ToList();
                    foreach (var i in ugh)
                    {
                        _context.UserTriviaGifteds.Remove(i);
                        _context.SaveChanges();
                    }
                    var dats = 24 -  (int)(DateTime.Now - tr.StartDate).TotalHours;
                    date = dats.ToString() + " h";
                }
                else
                {
                    var dats = 24 - (int)(DateTime.Now - tr.StartDate).TotalHours;
                    date = dats.ToString() + " h";
                }
                var gifts = _context.TriviaGift.ToList();
                var re = new List<TriviaGiftsModel>();
                foreach(var x in gifts)
                {
                    var g = new TriviaGiftsModel();
                    if(x.Coins > 0)
                    {
                        g.Gift = "x" + x.Coins.ToString();
                    }
                    if (x.hearts > 0)
                    {
                        g.Gift = "x" + x.hearts.ToString();
                    }
                    if (x.Tickets > 0)
                    {
                        g.Gift = "x" + x.Tickets.ToString();
                    }
                    if (x.CorrectAnswers> 0)
                    {
                        g.Gift = "x" + x.CorrectAnswers.ToString();
                    }

                    g.Logo = x.Logo;
                    g.Id = x.Id;
                    re.Add(g);
                }
                var result = new TriviaModel
                {
                    Data = date,
                    Gifts = re,
                    Status = Status
                };
                return Ok(result);
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



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("TriviaPlayer")]
        public IActionResult TriviaPlayer([FromBody] UserTriviaModelID userIdModel)
        {
            try
            {
                if (userIdModel.GiftId == null || userIdModel.GiftId == 0)
                {
                    var u = new UserTriviaGifted
                    {
                        UserId = userIdModel.userId,
                        Status = true
                    };
                    _context.UserTriviaGifteds.Add(u);
                    _context.SaveChanges();

                    return Problem("Can't find the gift");

                }
                else
                {
                    var u = new UserTriviaGifted
                    {
                        TriviaGiftId = (int)userIdModel.GiftId,
                        UserId = userIdModel.userId,
                        Status = true
                    };
                    _context.UserTriviaGifteds.Add(u);
                    _context.SaveChanges();

                    var gift = _context.TriviaGift.Find(userIdModel.GiftId);

                    var user = _context.Users.Find(userIdModel.userId);
                    user.Coins += gift.Coins;
                    user.Hearts += gift.hearts;
                    user.Tickets += gift.Tickets;
                    user.CorrectAnswers += gift.CorrectAnswers;

                    _context.SaveChanges();
                    var count = 0;
                    if(gift.Coins >0)
                    {
                        count = gift.Coins;
                    }
                    if (gift.hearts > 0)
                    {
                        count = gift.hearts;
                    }
                    if (gift.Tickets > 0)
                    {
                        count = gift.Tickets;
                    }
                    if (gift.CorrectAnswers > 0)
                    {
                        count = gift.CorrectAnswers;
                    }
                    return Ok(new { Message = "Collected Successfully", Logo = gift.Logo, Count = count });


                }








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

