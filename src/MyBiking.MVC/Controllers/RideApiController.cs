using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;

namespace MyBiking.MVC.Controllers
{
    [ApiController]
    public class RideApiController:Controller
    {
        private readonly IMediator _mediator;

        public RideApiController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        [Route("api/ride/{rideDto}")]
        public IActionResult CreateRide([FromBody] RideDto rideDto)
        {
            var result = _mediator.Send(rideDto);
            return Ok();
        }
    }
}
