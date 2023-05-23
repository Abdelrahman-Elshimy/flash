using AutoMapper;
using FlCash.Data;
using FlCash.DTOs;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RankingController(IUnitOfWork unitOfWork,
            IMapper mapper,DatabaseContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        // get the users based on the rank
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetRank")]
        public IActionResult GetRank([FromBody] UserIdModel userIdModel)
        {
            try
            {
                // order the users based on points
                var users = _context.Users.Where(x => x.PointsInWeek > 0).OrderByDescending(x => x.PointsInWeek);
                // get the current user
                var use = _context.Users.Find(userIdModel.userId);
                
                // take 20 row 
                var myUsers = users.Take(20).ToList();
                // create new list of users
                var userRanks = new List<UserRank>();
                var count = 0;
                foreach(var r in myUsers)
                {
                    var userRank = new UserRank
                    {
                        Counter = count + 1,
                        Points = r.Points,
                        Image = r.Image,
                        Name = r.FirstName +  " " + r.LastName
                       
                    };
                    count++;
                    userRanks.Add(userRank);
                }
                var list = users.ToList();
                // get the order of the current user
                int index = list.FindIndex(c => c.Id == userIdModel.userId) + 1;
                var RankUser = new UserRank
                {
                    Points = use.PointsInWeek,
                    Counter = index,
                    Image = use.Image,
                    Name = use.FirstName + " " + use.LastName
                };

                var monthlyRanks = _context.Users.Where(x => x.PointsInMonth > 0).OrderByDescending(x => x.PointsInMonth);
                var myMonthlyUsers = monthlyRanks.Take(20).ToList();
                var userMonthlyRanks = new List<UserRank>();
                var countMonthly = 0;
                foreach (var r in myMonthlyUsers)
                {
                    var userRank = new UserRank
                    {
                        Counter = countMonthly + 1,
                        Points = r.PointsInMonth,
                        Image = r.Image,
                        Name = r.FirstName + " " + r.LastName

                    };
                    countMonthly++;
                    userMonthlyRanks.Add(userRank);
                }
                var RankMnthlyUser = new UserRank
                {
                    Points = use.PointsInMonth,
                    Counter = monthlyRanks.ToList().FindIndex(c => c.Id == userIdModel.userId) + 1,
                    Image = use.Image,
                    Name = use.FirstName + " " + use.LastName
                };
                return Ok(new { UsersWeeklyRank = userRanks, UserWeeklyRank = RankUser, UsersMonthlyRank = userMonthlyRanks , UserMonthlyRank = RankMnthlyUser });
            }
            catch(Exception e)
            {
                var error = new Error
                {
                    Message = "Failed operation get rank",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetWeeklyRank")]
        public IActionResult GetWeeklyRank([FromBody] UserIdModel userIdModel)
        {
            try
            {
                // order the users based on points
                var users = _context.Users.OrderByDescending(x => x.PointsInWeek);
                // get the current user
                var use = _context.Users.Find(userIdModel.userId);

                // take 20 row 
                var myUsers = users.Take(20).ToList();
                // create new list of users
                var userRanks = new List<UserRank>();
                var count = 0;
                foreach (var r in myUsers)
                {
                    var userRank = new UserRank
                    {
                        Counter = count + 1,
                        Points = r.Points,
                        Image = r.Image,
                        Name = r.FirstName + " " + r.LastName

                    };
                    count++;
                    userRanks.Add(userRank);
                }
                var list = users.ToList();
                // get the order of the current user
                int index = list.FindIndex(c => c.Id == userIdModel.userId) + 1;
                var RankUser = new UserRank
                {
                    Points = use.Points,
                    Counter = index,
                    Image = use.Image,
                    Name = use.FirstName + " " + use.LastName
                };
                return Ok(new { UsersRank = userRanks, UserRank = RankUser });
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    Message = "Failed operation get rank",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetMonthlyRank")]
        public IActionResult GetMonthlyRank([FromBody] UserIdModel userIdModel)
        {
            try
            {
                // order the users based on points
                var users = _context.Users.OrderByDescending(x => x.PointsInMonth);
                // get the current user
                var use = _context.Users.Find(userIdModel.userId);

                // take 20 row 
                var myUsers = users.Take(20).ToList();
                // create new list of users
                var userRanks = new List<UserRank>();
                var count = 0;
                foreach (var r in myUsers)
                {
                    var userRank = new UserRank
                    {
                        Counter = count + 1,
                        Points = r.Points,
                        Image = r.Image,
                        Name = r.FirstName + " " + r.LastName

                    };
                    count++;
                    userRanks.Add(userRank);
                }
                var list = users.ToList();
                // get the order of the current user
                int index = list.FindIndex(c => c.Id == userIdModel.userId) + 1;
                var RankUser = new UserRank
                {
                    Points = use.Points,
                    Counter = index,
                    Image = use.Image,
                    Name = use.FirstName + " " + use.LastName
                };
                return Ok(new { UsersRank = userRanks, UserRank = RankUser });
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    Message = "Failed operation get rank",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
