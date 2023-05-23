using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlCash.Data;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModesIntroesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        // Dependency injection
        public ModesIntroesController(IUnitOfWork unitOfWork,
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
        public IActionResult GetModesIntroes(int id)
        {
            try
            {
                var introes = _context.ModesIntros.Where(x => x.ModeId == id).ToList();
                if (introes.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No Introes Founded",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }

                return Ok(new ModeDetailsList { Status = "Ok", ModeDetailList = introes });
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get Introes",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
