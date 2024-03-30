using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.Ride;
using MyBiking.Application.Functions.Command.RideApi;

namespace MyBiking.MVC.Controllers
{
    //[ApiController]
    public class RideApiController:ControllerBase
    {
        private readonly IMediator _mediator;

        public RideApiController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost]
        [Route("api/ride")]
        public async Task<IActionResult> CreateRide([FromBody] RideDtoApiCommand rideDto)
        {
            Status result =await _mediator.Send(rideDto);
            if (result.StatusCode == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
