using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Functions.Query.Wheelie;
using MyBiking.Application.ViewModels;

namespace MyBiking.MVC.Controllers
{
    public class WheelieController : Controller
    {
        private readonly IMediator _mediator;

        public WheelieController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[ActionName("Ride")]
        //[Route("Wheelie/{id}")]
        public async Task<IActionResult> Index([FromRoute] int? id)
        {
            var result =await _mediator.Send(new WheelieRidesQuery() { RideId = id });

            var response = new WheelieRideModelView()
            {
                WheelieRideDtos = result
            };

            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _mediator.Send(new WheelieRideQuery() { WheelieRideId = id });

            return Ok(result);
        }
    }
}
