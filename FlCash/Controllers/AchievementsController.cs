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
    //[Authorize]

    public class AchievementsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AchievementsController(DatabaseContext context)
        {
            _context = context;
        }
        // Get all categories with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CollectAchievement")]
        public IActionResult CollectAchievement([FromBody] CompleteAchievement completeAchievement)
        {
            try
            {
                var user = _context.Users.Find(completeAchievement.UserId);
                if (user != null)
                {
                    var ach = _context.Achievements.Find(completeAchievement.AchievementId);
                    if (ach != null)
                    {
                        CollectedAchievement collectedAchievement = new()
                        {
                            AchievementId = completeAchievement.AchievementId,
                            UserId = completeAchievement.UserId
                        };
                        _context.CollectedAchievements.Add(collectedAchievement);
                        _context.SaveChanges();
                        return Ok(new { Status = "200", Message = "Achievement Collected Successfully" });
                    }
                    else {

                        var error = new Error
                        {
                            Message = "Failed operation",
                            StatusCode = 500
                        };
                        return Problem(error.ToString());
                    }

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
        // Get all categories with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CollectCoins")]
        public IActionResult CollectCoins([FromBody] UpdateCoinsModel updateCoinsModel)
        {
            try
            {
                var user = _context.Users.Find(updateCoinsModel.UserId);
                if (user != null)
                {
                    user.Coins += updateCoinsModel.Coins;
                    _context.SaveChanges();
                    return Ok(new { Status = "200", Message = "Coins Updated Successfully" });
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
            } catch
            {
                var error = new Error
                {
                    Message = "Failed operation get achievements",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
        // Get all categories with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetAchievements")]
        public IActionResult GetAchievements([FromBody] UserIdModel userIdModel)
        {
            try
            {
                var user = _context.Users.Find(userIdModel.userId);
                var achievements = _context.Achievements.ToList();
                List<AchievementsDTO> list = new();

                foreach (var ach in achievements)
                {
                    AchievementsDTO dTO = new()
                    {
                        Id = ach.Id,
                        Coins = ach.Coins,
                        Description = ach.Description,
                        Collected = false,
                        Logo = ach.Logo,
                        percentage = 0,
                        Name = ach.Name,
                        Status = ach.Status
                    };

                    var isCollected = _context.CollectedAchievements.Where(x => x.AchievementId == ach.Id && x.UserId == user.Id);
                    if (isCollected.Any())
                    {
                        dTO.Collected = true;
                    }
                    if (user.WiningQuestionCounter >= ach.NumberOfQuestion)
                    {
                        dTO.Status = true;
                        dTO.percentage = 100;
                    }
                    else
                    {
                        if (user.WiningQuestionCounter == 0)
                        {
                            dTO.percentage = 0;
                        }
                        else
                        {
                            double x = (double)user.WiningQuestionCounter / ach.NumberOfQuestion;
                            dTO.percentage = x * 100;
                        }

                    }
                    list.Add(dTO);
                }
                AchievementModel achievementModel = new()
                {
                    Status = "200",
                    achievements = list
                };

                return Ok(achievementModel);
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
