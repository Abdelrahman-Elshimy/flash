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
    public class QuestionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        // Dependency injection
        public QuestionController(IUnitOfWork unitOfWork,
            IMapper mapper, DatabaseContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        // Get all questions with answers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuestions([FromQuery] RequestParams requestParams)
        {
            try
            {
                var Questions = await _unitOfWork.Questions.GetPagedList(requestParams, include: q => q.Include(x => x.Answers).Where(x => x.Status == 2).Include(x => x.Category).Include(x => x.Level));
                var results = _mapper.Map<IList<QuestionDTO>>(Questions);
                if (Questions.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No Questions Founded",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                return Ok(results);
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

        // Get question with answers
        [HttpGet("{id:int}", Name = "GetQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetQuestion(int id)
        {
            try
            {
                var Question = await _unitOfWork.Questions.Get(q => q.Id == id && q.Status == 2, include: q => q.Include(x => x.Answers).Include(x => x.Category).Include(x => x.Level));
                var result = _mapper.Map<QuestionDTO>(Question);
                if (result == null)
                {
                    var error = new Error
                    {
                        Message = "No Question With this Id",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                return Ok(result);
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

        // Get random questions with answers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("RandomQuestion")]
        public IActionResult RandomQuestion([FromBody] RandomQuestionRequest randomQuestionRequest)
        {

            try
            {
                

                List<Question> qs = new();
                foreach (long i in randomQuestionRequest.QuestionList)
                {
                    qs.Add(_context.Questions.Find(i));
                }
                var questions = _context.Questions
                        .Where(x => x.Status == 2 && x.Image == null)
                        .Include(x => x.Answers)
                        .Include(x => x.Category)
                        .Include(x => x.Level)
                        .OrderBy(c => Guid.NewGuid())
                        .ToList()
                        .Except(qs);
                if (randomQuestionRequest.CategoryId != 0 && _context.Categories.Find(randomQuestionRequest.CategoryId) != null)
                {
                    questions = questions.Where(x => x.CategoryId == randomQuestionRequest.CategoryId);
                }
                if (randomQuestionRequest.LevelId != 0 && _context.Levels.Find(randomQuestionRequest.LevelId) != null)
                {
                    questions = questions.Where(x => x.LevelId == randomQuestionRequest.LevelId);
                }
                questions = questions.Take(randomQuestionRequest.PageSize);
                var results = _mapper.Map<IList<QuestionAnswersDTO>>(questions);
                if (results != null)
                {
                    foreach (var re in results)
                    {
                        for (int i = 0; i < re.Answers.Count; i++)
                        {
                            if (re.Answers.ToList()[i].Name == "كل ما ورداعلاه" || re.Answers.ToList()[i].Name == "لا شيء مما سبق" || re.Answers.ToList()[i].Name == "لا شيء صحيح" || re.Answers.ToList()[i].Name == "كل ما سبق." || re.Answers.ToList()[i].Name == "كل ما سبق" || re.Answers.ToList()[i].Name == "كل ما ورداعلاه" || re.Answers.ToList()[i].Name == "لا شيء مما بالأعلى" || re.Answers.ToList()[i].Name == "لا شيء" || re.Answers.ToList()[i].Name == "لا شيء مما بالأعلى" || re.Answers.ToList()[i].Name == "كل الإجابات خاطئة")
                            {
                                re.Answers.Add(re.Answers.ToList()[i]);
                                re.Answers.Remove(re.Answers.ToList()[i]);
                            }
                        }
                    }
                    var res = new RandomQuestions
                    {
                        Status = "Ok",
                        Questions = results
                    };
                    return Ok(res);
                }
                else
                {
                    var error = new Error
                    {
                        StatusCode = 403,
                        Message = "No Questions exist"
                    };
                    return Problem(error.ToString());
                }

            }catch
            {
                var err = new Error
                {
                    Message = "Failed to fetch data",
                    StatusCode = 500
                };
                return Problem(err.ToString());
            }


        }


        // check if the anser is correct
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("IsAnswerCorrect")]
        public IActionResult IsAnswerCorrect(long? questionId, long? answerId)
        {
            try
            {
                if (questionId != null && answerId != null)
                {
                    var checkAnswer = _context.Answers.FirstOrDefault(x => x.QuestionId == questionId && x.Id == answerId && x.Status == true);
                    if(checkAnswer != null)
                    {
                        return Ok(new { Status = true, Message = "Answer is correct" });
                    }
                    else
                    {
                        return NotFound(new { StatusCode = 404, Status = false, Message = "Answer is not correct" });
                    }
                }
                else
                {
                    var error = new Error
                    {
                        StatusCode = 500,
                        Message = "Please send question and answer"
                    };
                    return Problem(error.ToString());
                }
            }catch
            {
                var error = new Error
                {
                    StatusCode = 500,
                    Message = "We have a problem with send the status of answer"
                };
                return Problem(error.ToString());
            }
        }


        // Get suggest question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetSuggetQuestion")]
        public IActionResult GetSuggetQuestion()
        {
            try
            {
                var categories = _context.Categories.ToList();
                var resultCategory = _mapper.Map<List<BaseCategoryDTO>>(categories);
                var levels = _context.Levels.ToList();
                var resultlevels = _mapper.Map<List<BaseLevelDTO>>(levels);

                SuggestQuestionModel suggestQuestionModel = new SuggestQuestionModel()
                {
                    Categories = resultCategory,
                    Levels = resultlevels
                };

                return Ok(suggestQuestionModel);
            }
            catch
            {
                var error = new Error
                {
                    StatusCode = 500,
                    Message = "Can't send your suggestion"
                };
                return Problem(error.ToString());
            }
        }


        // Get suggest question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("VotedQuestions")]
        public IActionResult VotedQuestions()
        {
            try
            {
                var questions = _context.Questions.Where(x => x.Status == 1).ToList();
                var result = _mapper.Map<List<BaseQuestionDTO>>(questions);
               

                return Ok(new { Questions = result });
            }
            catch
            {
                var error = new Error
                {
                    StatusCode = 500,
                    Message = "Can't send your suggestion"
                };
                return Problem(error.ToString());
            }
        }


        // suggest a new question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("SuggestQuestion")]
        public async Task<IActionResult> SuggestQuestion([FromBody] SuggestQuestionDTO question)
        {
            try
            {
                var question1 = new Question()
                {
                    CategoryId = question.CategoryId,
                    LevelId = question.LevelId,
                    Name = question.Name,
                    Status = 0
                };
                await _unitOfWork.Questions.Insert(question1);
                await _unitOfWork.Save();

                SuggestedQuestion suggestedQuestion = new SuggestedQuestion()
                {
                    QuestionId = question1.Id,
                    UserId = question.UserId

                };

                _context.SuggestedQuestions.Add(suggestedQuestion);
                _context.SaveChanges();

                Answer correctAnswer = new Answer
                {
                    Name = question.CorrectAnswer,
                    QuestionId = question1.Id,
                    Status = true
                };
                await _unitOfWork.Answers.Insert(correctAnswer);
                await _unitOfWork.Save();

                Answer Answer2 = new Answer
                {
                    Name = question.Answer2,
                    QuestionId = question1.Id,
                    Status = false
                };
                await _unitOfWork.Answers.Insert(Answer2);
                await _unitOfWork.Save();

                Answer Answer3 = new Answer
                {
                    Name = question.Answer3,
                    QuestionId = question1.Id,
                    Status = false
                };
                await _unitOfWork.Answers.Insert(Answer3);
                await _unitOfWork.Save();

                Answer Answer4 = new Answer
                {
                    Name = question.Answer4,
                    QuestionId = question1.Id,
                    Status = false
                };
                await _unitOfWork.Answers.Insert(Answer4);
                await _unitOfWork.Save();

                return Ok(new { Message = "Your suggestion question sent" });
            }catch
            {
                var error = new Error
                {
                    StatusCode = 500,
                    Message = "Can't send your suggestion"
                };
                return Problem(error.ToString());
            }
        }

        // rate question from 1 to 5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("RateQuestion")]
        public async Task<IActionResult> RateQuestion(QuestionRateDTO rate)
        {
            try
            {
                var rates = _context.QuestionRates.FirstOrDefault(x => x.UserId == rate.UserId && x.QuestionId == rate.QuestionId);
                var question = _context.Questions.Find(rate.QuestionId);
                if (rates != null)
                {
                    rates.Rate = rate.Rate;
                    await _context.SaveChangesAsync();
                    
                }
                else
                {
                    var nRate = _mapper.Map<QuestionRate>(rate);
                    _context.QuestionRates.Add(nRate);
                    await _context.SaveChangesAsync();
                }
                
                var count = _context.QuestionRates.Where(x => x.QuestionId == rate.QuestionId).Count();
                var sum = _context.QuestionRates.Sum(x => x.Rate);
                var MaxRate = sum / count;
                question.Rate = MaxRate;
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Rate updated successfully" });
            }
            catch
            {
                var error = new Error
                {
                    Message = "you can't rate now",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        // Get suggest question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetQuestionsSuggested")]
        public IActionResult GetQuestionsSuggested([FromBody] UserIdModel userIdModel)
        {
            try
            {
                var suggestedQuestions = _context.SuggestedQuestions.Where(x => x.UserId == userIdModel.userId).ToList();
                if (suggestedQuestions.Count > 0)
                {
                    List<Question> myQuestions = new List<Question>();
                    foreach (var s in suggestedQuestions)
                    {
                        var q = _context.Questions.Find(s.QuestionId);
                        myQuestions.Add(q);
                    }
                    var result = _mapper.Map<List<BaseQuestionDTO>>(myQuestions);

                    return Ok(new { SuggestedQuestions = result });
                }

                else
                {
                    var error = new Error
                    {
                        StatusCode = 403,
                        Message = "Can't get your suggestion"
                    };
                    return NotFound(error.ToString());
                }
                
            }
            catch
            {
                var error = new Error
                {
                    StatusCode = 500,
                    Message = "Can't get your suggestion"
                };
                return Problem(error.ToString());
            }
        }
    }
}
