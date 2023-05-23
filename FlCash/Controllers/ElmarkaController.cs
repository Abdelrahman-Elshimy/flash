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

    public class ElmarkaController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ElmarkaController(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("UpdateKey")]
        public IActionResult UpdateKey([FromBody] UserIdIndexElmarkaModel userIdModel)
        {
            try
            {
                var elmarka = _context.Elmarkas.Include(x => x.ElmarkaTriesUsers).FirstOrDefault(q => q.UserId == userIdModel.UserId);
                var tr = elmarka.ElmarkaTriesUsers.FirstOrDefault(x => x.Index == userIdModel.Index);
                tr.Status = 2;
                _context.SaveChanges();
                if (elmarka != null)
                {
                    elmarka.Keys += 1;
                    _context.SaveChanges();
                    return Ok(new { Message = "Update Successfully" });
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
        // Get Category with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("UpdateTries")]
        public IActionResult UpdateTries([FromBody] UpdateTriesModel updateTriesModel)
        {
            try
            {
                var elmarka = _context.Elmarkas.Include(q => q.ElmarkaTriesUsers).FirstOrDefault(q => q.UserId == updateTriesModel.userId);

                var tr = elmarka.ElmarkaTriesUsers.FirstOrDefault(q => q.Index == updateTriesModel.Index);

                if(tr.Tries == 0)
                {
                    return Ok(new { Message = "already updated" });
                }
                else
                {
                    tr.Tries -= 1;
                    _context.SaveChanges();
                    if(tr.Tries == 0)
                    {
                        tr.Status = 1;
                        _context.SaveChanges();
                    }
                    return Ok(new { Message = "Update Successfully" });
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
        [HttpPost(Name = "ElmarkaStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ElmarkaStatus([FromBody] UserIdModel userIdModel)
        {
            try
            {

                var elmarka = _context.Elmarkas.FirstOrDefault(q => q.UserId == userIdModel.userId);
                var remainingTime = _context.RemainingTimes.FirstOrDefault();
                if (elmarka != null)
                {
                    if ((remainingTime.StartDate.AddDays(7) - DateTime.Now).TotalSeconds > 0)
                    {

                        var elmarkaExist = _context.Elmarkas.Include(x => x.ElmarkaTriesUsers).FirstOrDefault(x => x.UserId == userIdModel.userId);
                        var d = remainingTime.StartDate.AddDays(7) - DateTime.Now;
                        var days = d.Days;
                        var hours = d.Hours;
                        var seconds = d.Seconds;
                        decimal percentege = 0;
                        var xx = 6;
                        if (elmarkaExist.Keys == 0)
                        {
                            percentege = 0;
                        }
                        else
                        {
                            decimal c = elmarkaExist.Keys / 6m;
                            percentege = c * 100;
                        }
                        var result = new ElmarkaDTO
                        {
                            Keys = elmarkaExist.Keys,
                            Percentage = percentege,
                            RemainDate = days.ToString() + "d " + hours.ToString() + "h",
                            Tries = _mapper.Map<List<ElmarkaTriesUserDTO>>(elmarkaExist.ElmarkaTriesUsers)
                        };
                        return Ok(result);
                    }
                    else
                    {
                        remainingTime.StartDate = DateTime.Now;
                        _context.SaveChanges();
                        var els = _context.Elmarkas.ToList();
                        foreach (var el in els)
                        {
                            _context.Elmarkas.Remove(el);
                            _context.SaveChanges();
                        }

                        var elma = new Elmarka
                        {
                            Keys = 0,
                            UserId = userIdModel.userId,

                        };
                        _context.Elmarkas.Add(elma);
                        _context.SaveChanges();

                        for (int i = 0; i < 6; i++)
                        {
                            var ElmUser = new ElmarkaTriesUser
                            {
                                ElmarkaGiftId = _context.ElmarkaGifts.ToList()[i].Id,
                                Index = i,
                                Tries = 3,
                                ElmarkaId = elma.Id,
                                CategoryId = _context.Categories.ToList()[i].Id,
                                Status = 0
                            };

                            _context.ElmarkaTriesUsers.Add(ElmUser);
                            _context.SaveChanges();
                        }


                        var d = remainingTime.StartDate.AddDays(7) - DateTime.Now;
                        var days = d.Days;
                        var hours = d.Hours;
                        var seconds = d.Seconds;
                        decimal percentege = 0;
                        if (elma.Keys == 0)
                        {
                            percentege = 0;
                        }
                        else
                        {
                            decimal c = elma.Keys / 6m;
                            percentege = c * 100;
                        }
                        var result = new ElmarkaDTO
                        {
                            Keys = elma.Keys,
                            Percentage = percentege,
                            RemainDate = days.ToString() + "d " + hours.ToString() + "h",
                            Tries = _mapper.Map<List<ElmarkaTriesUserDTO>>(elma.ElmarkaTriesUsers)
                        };
                        return Ok(result);


                    }

                }
                else
                {

                    var remTime = _context.RemainingTimes.ToList();
                    if (remTime.Count <= 0)
                    {
                        var re = new RemainingTime
                        {
                            StartDate = DateTime.Now
                        };

                        _context.RemainingTimes.Add(re);
                        _context.SaveChanges();
                    }



                    var elma = new Elmarka
                    {
                        Keys = 0,
                        UserId = userIdModel.userId,
                    };
                    _context.Elmarkas.Add(elma);
                    _context.SaveChanges();

                    for (int i = 0; i < 6; i++)
                    {
                        var ElmUser = new ElmarkaTriesUser
                        {
                            ElmarkaGiftId = _context.ElmarkaGifts.ToList()[i].Id,
                            Index = i,
                            Tries = 3,
                            ElmarkaId = elma.Id,
                            CategoryId = _context.Categories.ToList()[i].Id
                        };

                        _context.ElmarkaTriesUsers.Add(ElmUser);
                        _context.SaveChanges();
                    }


                    var d = _context.RemainingTimes.First().StartDate.AddDays(7) - DateTime.Now;
                    var days = d.Days;
                    var hours = d.Hours;
                    var seconds = d.Seconds;
                    decimal percentege = 0;
                    if (elma.Keys == 0)
                    {
                        percentege = 0;
                    }
                    else
                    {
                        decimal c = elma.Keys / 6m;
                        percentege = c * 100;
                    }
                    var result = new ElmarkaDTO
                    {
                        Keys = elma.Keys,
                        Percentage = percentege,
                        RemainDate = days.ToString() + "d " + hours.ToString() + "h",
                        Tries = _mapper.Map<List<ElmarkaTriesUserDTO>>(elma.ElmarkaTriesUsers)
                    };
                    return Ok(result);

                }
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get GetElmarka",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
