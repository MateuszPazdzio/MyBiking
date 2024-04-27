using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateRide([FromBody] RideDtoApiCommand rideDto)
        {
            Status result =await _mediator.Send(rideDto);
            result.Message = "Api error";
            if (result.Code != Entity.Enums.Code.HTTP201)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }
    }
}
