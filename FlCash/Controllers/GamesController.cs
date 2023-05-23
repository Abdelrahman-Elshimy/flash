using AutoMapper;
using FlCash.Data;
using FlCash.DTOs;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public GamesController(IUnitOfWork unitOfWork,
            IMapper mapper, DatabaseContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        // Get all games with users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGames([FromQuery] RequestParams requestParams)
        {
            try
            {
                var games = await _unitOfWork.Games.GetPagedList(requestParams, include: q => q.Include(x => x.Users));
                if (games.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No Games Founded",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                return Ok(games);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get games",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        // Get game with users
        [HttpGet("{id:int}", Name = "GetGame")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetGame(int id)
        {
            try
            {

                var game = _context.Games.Include(x => x.Users).Include(x => x.Questions).Include("Questions.Answers").FirstOrDefault(x => x.Id == id);
                if (game == null)
                {
                    var error = new Error
                    {
                        Message = "No game With this Id",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                var questionResult = _mapper.Map<List<QuestionAnswersDTO>>(game.Questions);
                var resultUsers = _mapper.Map<List<BaseUs>>(game.Users);
                var GameO = new CheckGameWithQuestionAndUsers
                {
                    GameID = id,
                    CounterOfUsers = game.Counter,
                    GameQuestions = questionResult,
                    Users = resultUsers,
                };
                return Ok(GameO);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get game",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
        // add player to game if exist if not exist create a new game
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("DeleteGames")]
        public IActionResult DeleteGames()
        {
            var games = _context.Games.ToList();
            foreach (var i in games)
            {
                _context.Games.Remove(i);
                _context.SaveChanges();
            }
            return Ok(new { Message = "Deleted" });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CorrectUsers")]
        public IActionResult CorrectUsers(string userId, long gameId)
        {
            var correctUser = _context.CorrectUsers.Where(x => x.GameId == gameId).FirstOrDefault(x => x.UserId == userId);
            if (correctUser != null)
            {
                var correct = new CorrectUser
                {
                    GameId = gameId,
                    UserId = userId
                };
                _context.CorrectUsers.Add(correct);
                _context.SaveChanges();
                return Ok(new { Message = "User Added Successfullt" });
            }
            else
            {
                return Ok(new { Message = "Correct user already" });
            }
        }
        // add player to game if exist if not exist create a new game
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("DeleteUsers")]
        public IActionResult DeleteUsers()
        {
            var friends = _context.Friends.ToList();
            foreach (var i in friends)
            {
                _context.Friends.Remove(i);
                _context.SaveChanges();
            }
            var games = _context.Users.ToList();
            foreach (var i in games)
            {
                _context.Users.Remove(i);
                _context.SaveChanges();
            }
            return Ok(new { Message = "Deleted" });
        }
        // add player to game if exist if not exist create a new game
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CheckGame")]
        public IActionResult CheckGame([FromBody] CheckGame checkGame)
        {
            try
            {
                var games = _context.Games.Include(x => x.Users).Include(x => x.Questions).Include("Questions.Answers").ToList();
                foreach (var g in games)
                {
                    foreach (var u in g.Users)
                    {
                        if (u.Id == checkGame.UserId && g.Status == false)
                        {
                            var questionResult = _mapper.Map<List<QuestionAnswersDTO>>(g.Questions);
                            g.Users.Remove(_context.Users.Find(checkGame.UserId));
                            var resultUsers = _mapper.Map<List<BaseUs>>(g.Users);

                            var GameO = new CheckGameWithQuestionAndUsers
                            {
                                GameID = g.Id,
                                CounterOfUsers = g.Counter,
                                GameQuestions = questionResult,
                                Users = resultUsers,
                            };
                            return Ok(GameO);
                        }
                    }
                }
                var game = _context.Games.Where(x => x.Status == false).Include(x => x.Users).Include(x => x.Questions).FirstOrDefault(x => x.Counter <= 1);

                if (game == null)
                {
                    var questions = _context.Questions
                        .Where(x => x.Status == 2 && x.Image == null)
                        .Include(x => x.Answers)
                        .OrderBy(c => Guid.NewGuid())
                        .ToList()
                        .Take(50);
                    var results = _mapper.Map<IList<Question>>(questions);
                    var user = _context.Users.FirstOrDefault(x => x.Id == checkGame.UserId);
                    var users = new List<ApiUser>
                    {
                        user
                    };
                    var nwGame = new Game
                    {
                        Counter = 1,
                        ModeId = checkGame.ModeId,
                        Questions = (List<Question>)results,
                        Users = users
                    };

                    _context.Games.Add(nwGame);
                    _context.SaveChanges();

                    var newGame = _context.Games.Include(x => x.Users).Include(x => x.Questions).Include("Questions.Answers").FirstOrDefault(x => x.Id == nwGame.Id);
                    newGame.Users.Remove(_context.Users.Find(checkGame.UserId));
                    var questionResult = _mapper.Map<List<QuestionAnswersDTO>>(nwGame.Questions);
                    var resultUsers = _mapper.Map<List<BaseUs>>(newGame.Users);
                    var GameO = new CheckGameWithQuestionAndUsers
                    {
                        GameID = newGame.Id,
                        CounterOfUsers = newGame.Counter,
                        GameQuestions = questionResult,
                        Users = resultUsers,
                    };
                    return Ok(GameO);

                }
                else
                {

                    game.Counter += 1;

                    //await _context.GameUsers.AddAsync(new GameUser { GameId = game.Id, ApiUserId = checkGame.UserId });
                    //await _context.SaveChangesAsync();
                    var user = _context.Users.FirstOrDefault(x => x.Id == checkGame.UserId);
                    var users = game.Users;
                    users.Add(user);
                    _context.SaveChanges();

                    if (game.Counter == 2)
                    {
                        game.Status = true;
                        _context.SaveChanges();
                    }

                    var newGame = _context.Games.Include(x => x.Users).Include(x => x.Questions).Include("Questions.Answers").FirstOrDefault(x => x.Id == game.Id);
                    var questionResult = _mapper.Map<List<QuestionAnswersDTO>>(newGame.Questions);
                    newGame.Users.Remove(_context.Users.Find(checkGame.UserId));
                    var resultUsers = _mapper.Map<List<BaseUs>>(newGame.Users);
                    var GameOo = new CheckGameWithQuestionAndUsers
                    {
                        GameID = newGame.Id,
                        CounterOfUsers = newGame.Counter,
                        GameQuestions = questionResult,
                        Users = resultUsers,
                    };
                    return Ok(GameOo);
                }


            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
        // delete player to game if exist
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetUserGame")]
        public IActionResult GetUserGame(string userId)
        {
            var games = _context.Games.Include(x => x.Users).ToList();
            var counter = 0;
            foreach (var g in games)
            {
                foreach (var u in g.Users)
                {
                    if (u.Id == userId)
                    {
                        counter++;
                    }
                }
            }
            if (counter == 0)
            {
                return Ok(_context.Users.Find(userId));
            }
            else
            {
                return Ok(new { Message = "Existed" });
            }
        }
        // delete player to game if exist
        ////[HttpDelete]
        ////[ProducesResponseType(StatusCodes.Status200OK)]
        ////[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        ////[Route("DeletePlayer")]
        ////public IActionResult DeletePlayer(string userId)
        ////{
        ////    try
        ////    {
        ////        return Ok(new { Message = "Game updated successfully" });
        ////        //var gameUser = _context.GameUsers.FirstOrDefault(x => x.ApiUserId == userId);

        ////        //if(gameUser != null)
        ////        //{
        ////        //    //_context.GameUsers.Remove(gameUser);
        ////        //    //await _context.SaveChangesAsync();
        ////        //    //var game = _context.Games.FirstOrDefault(x => x.Id == gameUser.GameId);
        ////        //    //game.Counter -= 1;
        ////        //    //await _context.SaveChangesAsync();

        ////        //    return Ok(new { Message = "Game updated successfully" });
        ////        //}
        ////        //else
        ////        //{
        ////        //    var error = new Error
        ////        //    {
        ////        //        Message = "Failed operation",
        ////        //        StatusCode = 500
        ////        //    };
        ////        //    return Problem(error.ToString());
        ////        //}
        ////    }
        ////    catch
        ////    {
        ////        var error = new Error
        ////        {
        ////            Message = "Failed operation",
        ////            StatusCode = 500
        ////        };
        ////        return Problem(error.ToString());
        ////    }
        ////}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetAllAnswers")]
        public IActionResult GetAllAnswers()
        {
            var qs = _context.QuestionResults.First();
            var m = _mapper.Map<ResultQuestionDTO>(qs);
            return Ok(m);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GameQuestionResult")]
        public IActionResult GameQuestionResult(long gameId, int index, long questionId)
        {

            var quresut = _context.QuestionResults.Where(x => x.GameId == gameId && x.QuestionId == questionId).FirstOrDefault();
            if (quresut != null)
            {
                if (index > 0)
                {
                    if (index == 1)
                    {
                        quresut.CountOfAnswerOne += 1;
                        _context.SaveChanges();
                    }
                    if (index == 2)
                    {
                        quresut.CountOfAnswerTwo += 1;
                        _context.SaveChanges();
                    }
                    if (index == 3)
                    {
                        quresut.CountOfAnswerThree += 1;
                        _context.SaveChanges();
                    }
                    if (index == 4)
                    {
                        quresut.CountOfAnswerFour += 1;
                        _context.SaveChanges();
                    }
                    return Ok(new { Message = "Updated" });
                }
                else
                {
                    List<int> vs = new();
                    vs.Add(quresut.CountOfAnswerOne);
                    vs.Add(quresut.CountOfAnswerTwo);
                    vs.Add(quresut.CountOfAnswerThree);
                    vs.Add(quresut.CountOfAnswerFour);
                    return Ok(vs);
                }

            }
            else
            {
                var q = new QuestionResult
                {
                    GameId = gameId,
                    QuestionId = questionId,
                    CountOfAnswerOne = 0,
                    CountOfAnswerTwo = 0,
                    CountOfAnswerThree = 0,
                    CountOfAnswerFour = 0
                };
                if (index == 1)
                {
                    q.CountOfAnswerOne += 1;
                }
                if (index == 2)
                {
                    q.CountOfAnswerTwo += 1;
                }
                if (index == 3)
                {
                    q.CountOfAnswerThree += 1;
                }
                if (index == 4)
                {
                    q.CountOfAnswerFour += 1;
                }
                _context.QuestionResults.Add(q);
                _context.SaveChanges();

                return Ok(new { Message = "Added" });
            }


        }

        // delete game if exist
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("DeleteGame")]
        public async Task<IActionResult> DeleteGame(long gameId)
        {
            try
            {
                var game = _context.Games.FirstOrDefault(x => x.Id == gameId);

                if (game != null)
                {
                    _context.Games.Remove(game);
                    await _context.SaveChangesAsync();
                   
                    return Ok(new { Message = "Game Removed successfully" });
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
                    Message = "Failed operation",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
