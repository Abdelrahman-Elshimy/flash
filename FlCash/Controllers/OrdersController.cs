using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlCash.Data;
using FlCash.DTOs;
using FlCash.Models;
using FlCash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public OrdersController(IUnitOfWork unitOfWork,
            IMapper mapper, DatabaseContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        // Get Category with question
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("CreateOrder")]
        public IActionResult CreateOrder([FromBody] OrderModel orderModel)
        {
            try
            {

                var order = new Order
                {
                    Date_Created = DateTime.Now,
                    OrderNumber = "test",
                    Status = "Pending",
                    StoreServicePlanId = orderModel.StoreServicePlanId,
                    UserId = orderModel.UserId
                };

                _context.Orders.Add(order);
                _context.SaveChanges();
                var or = new OrderRsponse
                {
                    Status = "200",
                    Message = "order addedd successfully",
                    OrderId = order.Id
                };
                return Ok(or);
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("ActivateOrder")]
        public IActionResult ActivateOrder([FromBody] ActivateOrderId activateOrderId)
        {
            try
            {

                var order = _context.Orders.Find(activateOrderId.OrderId);
                if (order == null)
                {
                    var error = new Error
                    {
                        Message = "No Order Founded",
                        StatusCode = 403
                    };
                    return NotFound(error.ToString());
                }
                var storeServicePlan = _context.StoreServicePlans.FirstOrDefault(x => x.Id == order.StoreServicePlanId);

                var storeService = _context.StoreServices.Find(storeServicePlan.StoreServiceId);

                var user = _context.Users.Find(order.UserId);

                if(storeService.Name == "Correct Answers")
                {
                    user.CorrectAnswers += storeServicePlan.Count;
                }
                if (storeService.Name == "Hearts")
                {
                    user.Hearts += storeServicePlan.Count;
                }

                if (storeService.Name == "Coins")
                {
                    user.Coins += storeServicePlan.Count;
                }
                if (storeService.Name == "Tickets")
                {
                    user.Tickets += storeServicePlan.Count;
                }

                _context.SaveChanges();

                order.Status = "Payed";
                _context.SaveChanges();

                return Ok(new { Status = "200", Message = "Order Activated successfully" });
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
    }
}
