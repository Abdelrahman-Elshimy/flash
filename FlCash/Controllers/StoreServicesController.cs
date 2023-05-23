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

    public class StoreServicesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreServicesController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get all categories with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStoreServices([FromQuery] RequestParams requestParams)
        {
            try
            {
                var StoreServices = await _unitOfWork.StoreServices.GetPagedList(requestParams, include: q => q.Include(x => x.StoreServicePlans));
                var results = _mapper.Map<IList<StoreServicesDTO>>(StoreServices);
                
                if (StoreServices.Count == 0)
                {
                    var error = new Error
                    {
                        Message = "No Store Services Founded",
                        StatusCode = 403
                    };
                    return NotFound(error.ToString());
                }
                foreach(var re in results)
                {
                    foreach(var s in re.StoreServicePlans)
                    {
                        s.Image = re.Logo;
                    }
                }
                StoreModel storeModel = new()
                {
                    Status = "200",
                    Stores = results
                };
                return Ok(storeModel);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get Store Services",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }

        // Get Category with question
        [HttpGet("{id:int}", Name = "GetStoreService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStoreService(int id)
        {
            try
            {

                var StoreService = await _unitOfWork.StoreServices.Get(q => q.Id == id, include: q => q.Include(x => x.StoreServicePlans));
                var result = _mapper.Map<StoreServicesDTO>(StoreService);
                if (result == null)
                {
                    var error = new Error
                    {
                        Message = "No Store Service With this Id",
                        StatusCode = 500
                    };
                    return NotFound(error.ToString());
                }
                return Ok(result);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get Store Service",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
