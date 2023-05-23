using AutoMapper;
using FlCash.Data;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class PrizesController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public PrizesController(DatabaseContext context)
        {

            _context = context;
        }
        // Get all categories with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CollectCoins")]
        public IActionResult CollectCoins([FromBody] CollectCoins collectCoins)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == collectCoins.UserId);
                user.Points += collectCoins.WrongAnswers;
                user.Points += collectCoins.CountOfQuestions * 3;
                user.PointsInWeek += collectCoins.WrongAnswers;
                user.PointsInWeek += collectCoins.CountOfQuestions * 3;
                user.PointsInMonth += collectCoins.WrongAnswers;
                user.PointsInMonth += collectCoins.CountOfQuestions * 3;

                user.PlayedGamesCounter += 1;
                user.RightCounterCounter += collectCoins.CountOfQuestions;
                _context.SaveChanges();


                var re = new PrizesModel
                {
                    Message = "Coins Collected Successfully",
                    GainedCoins = collectCoins.WrongAnswers + (collectCoins.CountOfQuestions * 3),
                };

                return Ok(re);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation Collect Prizes",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        // Get all categories with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("DecreasingUserStore")]
        public IActionResult DecreasingUserStore([FromBody] DecreasingUserStoreModel decreasingUserStoreModel)
        {
            try
            {
                var user = _context.Users.Find(decreasingUserStoreModel.UserId);

                if (user == null)
                {
                    var error = new Error
                    {
                        Message = "No User Founded",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                user.Coins -= decreasingUserStoreModel.Coins;
                user.Hearts -= decreasingUserStoreModel.Hearts;
                user.Tickets -= decreasingUserStoreModel.Tickets;
                user.CorrectAnswers -= decreasingUserStoreModel.CorrectAnswers;
                _context.SaveChanges();

              
                return Ok(new { Message = "Store of user updated successfully" });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation Collect Prizes",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
        // Get all categories with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("UpdateUserResources")]
        public IActionResult UpdateUserResources([FromBody] UpdateUserStore updateUserStore)
        {
            try
            {
                var user = _context.Users.Find(updateUserStore.UserId);

                if (user == null)
                {
                    var error = new Error
                    {
                        Message = "No User Founded",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                user.Coins = updateUserStore.Coins;
                user.CorrectAnswers = updateUserStore.CorrectAnswers;
                _context.SaveChanges();


                return Ok(new { Message = "Store of user updated successfully" });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation Collect Prizes",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
 

}
