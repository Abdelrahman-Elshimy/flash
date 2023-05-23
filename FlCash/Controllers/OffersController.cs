using AutoMapper;
using FlCash.Data;
using FlCash.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlCash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class OffersController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public OffersController(DatabaseContext context)
        {
            _context = context;
        }
        // Get all categories with question
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetLastOffer")]
        public IActionResult GetLastOffer()
        {
            try
            {
                var offers = _context.Offers.ToList();
                var offerLen = offers.Count - 1;
                var offer = offers[offerLen];
                return Ok(offer);
            }
            catch
            {
                var error = new Error
                {
                    Message = "Failed operation get offer",
                    StatusCode = 500
                };
                return Problem(error.ToString());
            }
        }
    }
}
