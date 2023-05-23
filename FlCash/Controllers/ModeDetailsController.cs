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
    public class ModeDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        // Dependency injection
        public ModeDetailsController(IUnitOfWork unitOfWork,
            IMapper mapper, DatabaseContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("RemainingTime")]
        public IActionResult RemainingTime()
        {
            try
            {
                var ModeDetail = _context.ModeDetails.FirstOrDefault();
                var gifts = _context.Gifts.ToList();
                var remaingTime = ModeDetail.End - DateTime.Now;
                string Days = remaingTime.Days.ToString() + "d";
                string hours = remaingTime.Hours.ToString() + "h";
                if(remaingTime.Hours <= 0)
                {
                    ModeDetail.Start = DateTime.Now;
                    ModeDetail.End = DateTime.Now.AddDays(7);
                    _context.SaveChanges();
                    var ModeDetail1 = _context.ModeDetails.FirstOrDefault();
                    var remaingTime1 = ModeDetail1.End - DateTime.Now;
                    string Days1 = remaingTime1.Days.ToString() + "d";
                    string hours1 = remaingTime1.Hours.ToString() + "h";
                    ModeDetailsModel ModeDetailss1 = new ModeDetailsModel
                    {
                        Gifts = gifts,
                        RemainingDays = Days1 + " " + hours1
                    };
                    return Ok(ModeDetailss1);
                }
                ModeDetailsModel ModeDetailss = new ModeDetailsModel
                {
                    Gifts = gifts,
                    RemainingDays = Days + " " + hours
                };
                return Ok(ModeDetailss);

            }catch
            {
                var error = new Error
                {
                    StatusCode = 403,
                    Message = "Failed"
                };
                return Problem(error.ToString());
            }
        }
    }

}
