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

    public class NewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context; 

        public NewsController(IUnitOfWork unitOfWork,
            IMapper mapper, DatabaseContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        // Get all News with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetNews()
        {
            try
            {
                var News = _context.News.ToList();

                if (News.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No News Founded",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                var result = new NewsModel
                {
                    Status = "200",
                    News = News
                };
                return Ok(result);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get News",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        // Get New with question
        [HttpGet("{id:int}", Name = "GetNew")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNew(int id)
        {
            try
            {

                var New = await _unitOfWork.News.Get(q => q.Id == id);
                var result = _mapper.Map<New>(New);
                if (result == null)
                {
                    var error = new Error
                    {
                        Message = "No New With this Id",
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
                    Message = "Failed operation get News",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
