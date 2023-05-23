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

    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get all categories with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories([FromQuery] RequestParams requestParams)
        {
            try
            {
                var Categorys = await _unitOfWork.Categories.GetPagedList(requestParams, include: q => q.Include(x => x.Questions));
                var results = _mapper.Map<IList<BaseCategoryDTO>>(Categorys);
                if (Categorys.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No Categories Founded",
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
                    Message = "Failed operation get categories",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        // Get Category with question
        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {

                var Category = await _unitOfWork.Categories.Get(q => q.Id == id, include: q => q.Include(x => x.Questions));
                var result = _mapper.Map<GETCategoryDTO>(Category);
                if (result == null)
                {
                    var error = new Error
                    {
                        Message = "No Category With this Id",
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
                    Message = "Failed operation get categories",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
