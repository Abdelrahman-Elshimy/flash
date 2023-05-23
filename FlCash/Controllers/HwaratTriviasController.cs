using AutoMapper;
using FlCash.Data;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HwaratTriviasController : ControllerBase
    {

     

        private readonly DatabaseContext _context;
        // Dependency injection
        public HwaratTriviasController(DatabaseContext context)
        {
        
            _context = context;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetTriviaMissions")]
        public IActionResult GetTriviaMissions([FromQuery] UserIdModel userIdModel)
        {
            try
            {
                var trivi = _context.Trivias.ToList();
                string date = "";

                if (trivi.Count <= 0)
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
                if (DateTime.Now > tr.EndDate)
                {
                    tr.StartDate = DateTime.Now;
                    tr.EndDate = DateTime.Now.AddDays(1);

                    _context.SaveChanges();
                    var ugh = _context.UserTriviaGifteds.ToList();
                    var u = _context.HwaratTriviaMissionUsers.ToList();
                    foreach (var i in ugh)
                    {
                        _context.UserTriviaGifteds.Remove(i);
                        _context.SaveChanges();
                    }
                    foreach (var x in u)
                    {
                        _context.HwaratTriviaMissionUsers.Remove(x);
                        _context.SaveChanges();
                    }
                    var dats = 24 - (int)(DateTime.Now - tr.StartDate).TotalHours;
                    date = dats.ToString() + " h";
                }
                else
                {
                    var dats = 24 - (int)(DateTime.Now - tr.StartDate).TotalHours;
                    date = dats.ToString() + " h";
                }
                
                var missions = _context.Missions.ToList();
                
                List<UserMission> userMissions = new();
                List<MissionsModel> missionsModels = new();


                
                foreach (var mi in missions)
                {
                    var h = _context.HwaratTriviaMissionUsers.FirstOrDefault(x => x.UserId == userIdModel.userId && x.MissionId == mi.Id);
                    if(h == null)
                    {
                        var Ha = new HwaratTriviaMissionUser
                        {
                            MissionId = mi.Id,
                            CountOfQuestionsSolved = 0,
                            UserId = userIdModel.userId,

                        };
                        _context.HwaratTriviaMissionUsers.Add(Ha);
                        _context.SaveChanges();
                    }
                }

                var usetr = _context.HwaratTriviaMissionUsers.Include(x => x.Mission).Where(x => x.UserId == userIdModel.userId);

                foreach(var hu in usetr)
                {
                    var x = new UserMission
                    {
                        Id = hu.Id,
                        QuestionGained = hu.CountOfQuestionsSolved,
                        CategoryId = hu.Mission.CategoryId,
                        Coins = hu.Mission.Coins,
                        Description = hu.Mission.Description,
                        QuestionCount = hu.Mission.QuestionCount,
                        Status = hu.CountOfQuestionsSolved == hu.Mission.QuestionCount,
                        Percentage = ((decimal)hu.CountOfQuestionsSolved / (decimal)hu.Mission.QuestionCount ) * 100
                    };
                    userMissions.Add(x);
                }
                var counter = 0;

                foreach(var m in userMissions)
                {
                    if(m.Status)
                    {
                        counter += 1;
                    }
                }
                var percentage = (decimal)counter / 5m;
                percentage *= 100;
                return Ok(new { date, Percentage = percentage, Missions = userMissions });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get questions",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }


    }
}
