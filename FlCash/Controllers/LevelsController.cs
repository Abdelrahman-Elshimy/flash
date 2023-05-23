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
    public class LevelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LevelsController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get all categories with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLevels([FromQuery] RequestParams requestParams)
        {
            try
            {
                var Levels = await _unitOfWork.Levels.GetPagedList(requestParams, include: q => q.Include(x => x.Questions));
                var results = _mapper.Map<IList<BaseLevelDTO>>(Levels);
                if (Levels.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No Levels Founded",
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
                    Message = "Failed operation get levels",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        // Get Category with question
        [HttpGet("{id:int}", Name = "GetLevel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLevel(int id)
        {
            try
            {

                var Level = await _unitOfWork.Levels.Get(q => q.Id == id, include: q => q.Include(x => x.Questions));
                var result = _mapper.Map<LevelDTO>(Level);
                if (result == null)
                {
                    var error = new Error
                    {
                        Message = "No Level With this Id",
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
                    Message = "Failed operation get levels",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
