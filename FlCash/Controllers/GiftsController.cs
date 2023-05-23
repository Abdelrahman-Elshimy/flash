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

    public class GiftsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GiftsController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get all categories with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGifts()
        {
            try
            {
                var Categorys = await _unitOfWork.Gifts.GetAll();
                if (Categorys.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No Gifts Founded",
                        StatusCode = 500
                    };
                    return Problem(error.ToString());
                }
                return Ok(Categorys);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get gifts",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

}
    }
