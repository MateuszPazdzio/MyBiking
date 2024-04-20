using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.Ride;
using MyBiking.Application.Functions.Command.Wheelie;
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            Status result = await _mediator.Send(new DeleteWheelieCommand() { Id = id.Value });
            string returnUrl = Request.Headers["Referer"].ToString();
            if (result.StatusCode != 204)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect(returnUrl);
            }
        }
    }
}
