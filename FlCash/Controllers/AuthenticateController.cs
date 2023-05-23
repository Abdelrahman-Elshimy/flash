using AutoMapper;
using FlCash.Authentication;
using FlCash.Data;
using FlCash.DTOs;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public AuthenticateController(UserManager<ApiUser> userManager, IConfiguration configuration, DatabaseContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterNewUser model)
        {
            
            // check if user logined before
            var userExists = _context.Users.FirstOrDefault(x => x.Email == model.Email && x.FaceId == model.FaceId);
            if (userExists != null)
            
            {

                foreach (var friendFaceId in model.FriendsFacesId)
                {
                    var isUser = _context.Users.FirstOrDefault(x => x.FaceId == friendFaceId);
                    if (isUser != null)
                    {
                        var IsFriend = _context.Friends.FirstOrDefault(x => x.UserId == userExists.Id && x.FaceBookId == isUser.FaceId);
                        if (IsFriend == null)
                        {
                            var fs = new List<Friend>();
                            var friend = new Friend
                            {
                                UserId = userExists.Id,
                                FaceBookId = isUser.FaceId,
                                Status = true
                            };
                            fs.Add(friend);
                            var newFriend = new Friend
                            {
                                FaceBookId = userExists.FaceId,
                                UserId = isUser.Id,
                                Status = true
                            };
                            fs.Add(newFriend);
                            _context.Friends.AddRange(fs);
                            _context.SaveChanges();
                        }

                    }
                }
                var userExio2 = _context.Users.FirstOrDefault(x => x.Email == model.Email && x.FaceId == model.FaceId);
                if (userExio2 != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(userExio2);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userExio2.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Data Loaded Successfully";
                   
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        userId = userExio2.Id,
                        expiration = token.ValidTo,
                        Status = "Success",
                        Message = "Date loaded successfully!"
                    });
                }
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Data Loaded Failed!";

                return StatusCode(statusCode: 500, new Error { Message = "Data Loaded Failed!" });
            }
            ApiUser user = new()
            {
                FirstName = model.FirstName, 
                LastName = model.LastName,
                UserName = model.Email,
                Image = model.Image,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FaceId = model.FaceId,
                LastEnter = DateTime.Now,
                CounterDailyGift = 0,
                EasyLevelQuestionCount = 0,
                MediumLevelQuestionCount = 0,
                HardLevelQuestionCount = 0,
                ArtQuestionCount = 0,
                HistoryQuestionCount = 0,
                GeoQuestionCount = 0,
                EntertainmanetQuestionCount = 0,
                ScienceQuestionCount = 0,
                SportsQuestionCount = 0,
                Coins = 100
            };
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                foreach (var friendFaceId in model.FriendsFacesId)
                {
                    var isUser = _context.Users.FirstOrDefault(x => x.FaceId == friendFaceId);
                    if (isUser != null)
                    {
                        var IsFriend = _context.Friends.FirstOrDefault(x => x.UserId == user.Id && x.FaceBookId == isUser.FaceId);
                        if (IsFriend == null)
                        {
                            var friend = new Friend
                            {
                                UserId = user.Id,
                                FaceBookId = isUser.FaceId,
                                Status = true
                            };
                            _context.Friends.Add(friend);
                            await _context.SaveChangesAsync();
                            var newFriend = new Friend
                            {
                                FaceBookId = user.FaceId,
                                UserId = isUser.Id,
                                Status = true
                            };
                            _context.Friends.Add(newFriend);
                            await _context.SaveChangesAsync();
                        }

                    }
                }
            }
            catch
            {
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User creation failed! Please check user details and try again.";
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            var userExio = _context.Users.FirstOrDefault(x => x.Email == model.Email && x.FaceId == model.FaceId);
            if (userExio != null)
            {
                var userRoles = await _userManager.GetRolesAsync(userExio);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userExio.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User created successfully!";
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    userId = userExio.Id,
                    expiration = token.ValidTo,
                    Status = "Success",
                    Message = "User created successfully!"
                });
            }
            Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User created Failed!";
            return StatusCode(statusCode: 500, new Error { Message = "User created Failed!" });
        }

        [HttpPost]
        [Route("Edit")]
        [Authorize]
        public async Task<IActionResult> Edit(string id, [FromBody] EditUserDTO model)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                user.Image = model.Image;

                await _context.SaveChangesAsync();
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User updated successfully";
                return Ok(new { Message = "User updated successfully" });


            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation edit user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User updated Failed";
                return Problem(error.ToString());
            }
        }

        //[HttpPost]
        //[Route("GetUserStore")]
        //public IActionResult GetUserStore([FromBody] UserIdModel model)
        //{
        //    try
        //    {
        //        var user = _context.Users.Find(model.userId);
        //        if (user == null)
        //        {
        //            return BadRequest("Submitted data is invalid");
        //        }
        //        var result = _mapper.Map<ApiUser>(user);
        //        return Ok(result);


        //    }
        //    catch
        //    {
        //        var error = new Error
        //        {
        //            Message = "Failed operation",
        //            StatusCode = 500
        //        };
        //        Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Failed Operation";
        //        return Problem(error.ToString());
        //    }
        //}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("EditCoins")]
        public IActionResult EditCoins([FromBody] EditCoinsRequest editCoins)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == editCoins.Id);
                user.Coins = editCoins.Coins;
                _context.SaveChanges();
                return Ok(new { Message = "Coins updated successfully" });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation edit user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User updated Failed";
                return Problem(error.ToString());
            }
        }
                
        [HttpGet]
        [Route("UserDate")]
        public IActionResult RestoreUser()
        {
            var users = _context.Users.ToList();
            foreach(var user in users)
            {
                user.LastEnter = DateTime.Now;
                user.CounterDailyGift = 0;
                _context.SaveChanges();
            }
            return Ok(new { Message = "Done" });
        }
                // Get user data
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetUserStore")]
        public IActionResult GetUserStore([FromBody] UserIdModel model)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == model.userId);

                var ob = new UserStore
                {
                    Coins = user.Coins,
                    CorrectAnswers = user.CorrectAnswers,
                    Tickets = user.Tickets,
                    Hearts = user.Hearts

                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data loaded";
                return Ok(ob);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data not fetched successfully";
                return Problem(error.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("DialyGifts")]
        public IActionResult DialyGifts([FromBody] UserIdModel model)
        {
            try
            {
                List<UserGiftDaily> giftDailies = new();
                var user = _context.Users.FirstOrDefault(x => x.Id == model.userId);
                var dailyGifts = _context.DailyGifts.ToList();
                if(user.CounterDailyGift == dailyGifts.Count)
                {
                    user.CounterDailyGift = 0;
                    _context.SaveChanges();
                }
                var status = false;
                if ((DateTime.Now - user.LastEnter).Seconds > 10)
                {
                    for (int i = 0; i < dailyGifts.Count; i++)
                    {
                        if(user.CounterDailyGift == i)
                        {
                            var x = new UserGiftDaily
                            {
                                CanCollect = true,
                                Id = dailyGifts[i].Id,
                                Image = dailyGifts[i].Image,
                                IsCollected = false,
                                Message = dailyGifts[i].Message,
                                Value = dailyGifts[i].Value
                            };
                            giftDailies.Add(x);
                        }
                        else if(user.CounterDailyGift > i)
                        {
                            var x = new UserGiftDaily
                            {
                                CanCollect = false,
                                Id = dailyGifts[i].Id,
                                Image = dailyGifts[i].Image,
                                IsCollected = true,
                                Message = dailyGifts[i].Message,
                                Value = dailyGifts[i].Value
                            };
                            giftDailies.Add(x);
                        }
                        else
                        {
                            var x = new UserGiftDaily
                            {
                                CanCollect = false,
                                Id = dailyGifts[i].Id,
                                Image = dailyGifts[i].Image,
                                IsCollected = false,
                                Message = dailyGifts[i].Message,
                                Value = dailyGifts[i].Value
                            };
                            giftDailies.Add(x);
                        }

                    }
                }
                

                if(giftDailies.Count > 0)
                {
                    status = true;
                }
                
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data loaded";
                return Ok(new {Status = status,Counter = user.CounterDailyGift, DailyGits = giftDailies });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data not fetched successfully";
                return Problem(error.ToString());
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserModelDto model)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == model.userId);
                user.PhoneNumber = model.phoneNumber;
                user.UserName = model.name;
                user.Address = model.address;
                user.Governement = model.governement;
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data not fetched successfully";
                return Problem(error.ToString());
            }

        }

            // Get user data
            [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetUserData")]
        public async Task<IActionResult> GetUserData([FromBody] UserIdModel model)
        {
            try
            {
                List<GovernementDto> governorates = new List<GovernementDto>
                 {
                    new GovernementDto()
                    {
                        id = 1,
                        title = "القاهرة",
                    },
                    new GovernementDto()
                    {
                        id = 2,
                        title = "الإسكندرية",
                    },
                    new GovernementDto()
                    {
                        id = 3,
                        title = "البحيرة",
                    },
                    new GovernementDto()
                    {
                        id = 4,
                        title = "كفر الشيخ",
                    },
                    new GovernementDto()
                    {
                        id = 5,
                        title = "الدقهلية",
                    },
                    new GovernementDto()
                    {
                        id = 6,
                        title = "الشرقية",
                    },
                    new GovernementDto()
                    {
                        id = 7,
                        title = "بورسعيد",
                    },
                    new GovernementDto()
                    {
                        id = 8,
                        title = "الإسماعيلية",
                    },
                    new GovernementDto()
                    {
                        id = 9,
                        title = "السويس",
                    },
                    new GovernementDto()
                    {
                        id = 10,
                        title = "الغربية",
                    },
                    new GovernementDto()
                    {
                        id = 11,
                        title = "المنوفية",
                    },
                       new GovernementDto()
                    {
                        id = 12,
                        title = "جنوب سيناء",
                    },
                    new GovernementDto()
                    {
                        id = 13,
                        title = "شمال سيناء",
                    },
                             new GovernementDto()
                    {
                        id = 14,
                        title = "مطروح",
                    },
                                new GovernementDto()
                    {
                        id = 15,
                        title = "الوادي الجديد",
                    },
                                   new GovernementDto()
                    {
                        id = 16,
                        title = "البحر الأحمر",
                    },
                                      new GovernementDto()
                    {
                        id = 17,
                        title = "أسوان",
                    },
                                         new GovernementDto()
                    {
                        id = 18,
                        title = "الأقصر",
                    },
                                            new GovernementDto()
                    {
                        id = 19,
                        title = "قنا",

                    },
                    new GovernementDto()
                    {
                        id = 20,
                        title = "سوهاج",
                    },
                    new GovernementDto()
                    {
                        id = 21,
                        title = "أسيوط",
                    },
                    new GovernementDto()
                    {
                        id = 22,
                        title = "المنيا",
                    },
                    new GovernementDto()
                    {
                        id = 23,
                        title = "بني سويف",
                    },
                    new GovernementDto()
                    {
                        id = 24,
                        title = "الفيوم",
                    },

                };
                var user = _context.Users.Include(x => x.Friends).FirstOrDefault(x => x.Id == model.userId);
                var levels = _context.UserLevels.OrderByDescending(x => x.QuestionCount).ToList();
                var level = new UserLevel();

                foreach (var l in levels)
                {
                    if (user.WiningQuestionCounter < l.QuestionCount)
                    {
                        level = l;
                    }
                }

                decimal percentage = 0;
                if(user.WiningQuestionCounter > 0)
                {
                    percentage = ((decimal)user.WiningQuestionCounter / (decimal)level.QuestionCount) * 100;
                }
                var le = new UserLevelDTo
                {
                    Name = level.Name,
                    Percantage = percentage,
                    QuestionCounter = user.WiningQuestionCounter

                };
                var achs = await _unitOfWork.CollectedAchievements.GetAll(x => x.UserId == model.userId);
                List<Achievement> achievements = new();
                foreach (var ach in achs)
                {
                    var ac = await _unitOfWork.Achievements.Get(x => x.Id == ach.AchievementId);
                    achievements.Add(ac);
                }
                var reAchs = _mapper.Map<List<BaseAchievementsDTO>>(achievements);
                string gover = null;
                if(user.Governement != null)
                {
                    gover = user.Governement;
                }
                var newUser = new ProfileDTO
                {
                    Level = le,
                    Achievements = reAchs,
                    Governements = governorates,
                    address = user.Address,
                    governement = gover,
                    name = user.UserName,
                    phoneNumber = user.PhoneNumber

                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data loaded";
                return Ok(newUser);

                // test

                //"level": {
                //                  "name": "المستوي الأول",
                //  "percantage": 0,
                //  "questionCounter": 0
                //},
                //"achievements": [
                //  {
                //                  "logo": "https://flahcashapi.azurewebsites.net/Uploads/Achievements/39Group 7686.png"
                //  }
                //]
                //var achs = _context.Achievements.ToList();
                //var reAchs = _mapper.Map<List<BaseAchievementsDTO>>(achs);
                //var le = new UserLevelDTo
                //{
                //    Name = "المستوي ألاول",
                //    Percantage = 20,
                //    QuestionCounter = 2

                //};
                //var newUser = new ProfileDTO
                //{
                //    Level = le,
                //    Achievements = reAchs

                //};

                //return Ok(newUser);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data not fetched successfully";
                return Problem(error.ToString());
            }

        }

        // Get user data
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetUserFriends")]
        public IActionResult GetUserFriends([FromBody] UserIdModel model)
        {
            try
            {
                var user = _context.Users.Include(x => x.Friends).FirstOrDefault(x => x.Id == model.userId);
                List<ApiUser> userFriends = new();
                if (user == null)
                {
                    var error = new Error
                    {
                        Message = "No user founded",
                        StatusCode = 500
                    };
                    Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "No user founded";
                    return Problem(error.ToString());
                }
                foreach (var friend in user.Friends)
                {
                    userFriends.Add(_context.Users.FirstOrDefault(x => x.FaceId == friend.FaceBookId));
                }
                var newUserFriends = new List<FriendRequest>();
                foreach (var x in userFriends)
                {
                    var y = new FriendRequest
                    {
                        Name = x.FirstName + " " + x.LastName,
                        Image = x.Image
                    };
                    newUserFriends.Add(y);
                }
               
                var newUser = new FrindUserDTO
                {
                    Friends = newUserFriends
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data loaded";
                return Ok(newUser);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data not fetched successfully";
                return Problem(error.ToString());
            }

        }
        // Get user data
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetUserLevel")]
        public IActionResult GetUserLevel([FromBody] UserIdModel model)
        {
            try
            {
                var user = _context.Users.Include(x => x.Friends).FirstOrDefault(x => x.Id == model.userId);
                var levels = _context.UserLevels.OrderByDescending(x => x.QuestionCount).ToList();
                var level = new UserLevel();

                foreach(var l in levels)
                {
                    if(user.WiningQuestionCounter < l.QuestionCount)
                    {
                        level = l;
                    }
                }

                decimal percentage = ((decimal)user.WiningQuestionCounter / (decimal)level.QuestionCount) * 100;

                return Ok(new { Level = level.Name, Percentage = percentage });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data not fetched successfully";
                return Problem(error.ToString());
            }
        }

        // Get user data
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CollectDailyGifts")]
        public IActionResult CollectDailyGifts([FromBody] CollectDailyGift model)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == model.userId);
                var GiftsCounter = _context.DailyGifts.ToList().Count;

                user.CounterDailyGift += 1;
                user.LastEnter = DateTime.Now;

                var gift = _context.DailyGifts.Include(x => x.StoreService).FirstOrDefault(x => x.Id == model.GiftId);

                if (gift.StoreService.Name == "Correct Answers")
                {
                    user.CorrectAnswers += gift.Value;
                }
                if (gift.StoreService.Name == "Hearts")
                {
                    user.Hearts += gift.Value;
                }

                if (gift.StoreService.Name == "Coins")
                {
                    user.Coins += gift.Value;
                }
                if (gift.StoreService.Name == "Tickets")
                {
                    user.Tickets += gift.Value;
                }


                _context.SaveChanges();

                return Ok(new { Message = "Updated"  });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get user",
                    StatusCode = 500
                };
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "User data not fetched successfully";
                return Problem(error.ToString());
            }
        }


        [HttpPost]
        [Route("CollectPrizesFromMobile")]
        public IActionResult CollectPrizesFromMobile([FromBody] CollectPrizesFromMobileDto model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == model.userId);
            user.Points += model.wrongQuestions;
            user.Points += model.correctQuestions * 3;
            user.PointsInWeek += model.wrongQuestions;
            user.PointsInWeek += model.correctQuestions * 3;
            user.PointsInMonth += model.wrongQuestions;
            user.PointsInMonth += model.correctQuestions * 3;
            _context.SaveChanges();

            return Ok();
        }
    }
}