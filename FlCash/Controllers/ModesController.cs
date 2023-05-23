using AutoMapper;
using FlCash.Data;
using FlCash.DTOs;
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

    public class ModesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModesController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get all Modes with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetModes([FromQuery] RequestParams requestParams)
        {
            try
            {
                var Modes = await _unitOfWork.Modes.GetPagedList(requestParams, include: q => q.Include(x => x.ModesIntros));
                if (Modes.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No Modes Founded",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                return Ok(Modes);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get Modes",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        // Get Mode with question
        [HttpGet("{id:int}", Name = "GetMode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMode(int id)
        {
            try
            {

                var mode = await _unitOfWork.Modes.Get(q => q.Id == id, include: q => q.Include(x => x.ModesIntros));
                if (mode == null)
                {
                    var error = new Error
                    {
                        Message = "No Mode With this Id",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                return Ok(mode);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get Modes",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
