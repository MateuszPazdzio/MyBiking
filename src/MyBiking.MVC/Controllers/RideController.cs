using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Functions.Query.Ride;
using MyBiking.Application.Functions.Command.RideApi;
using MyBiking.Application.Dtos;
using MyBiking.Application.ViewModels;
using MyBiking.Application.Functions.Command.Ride;
using MyBiking.Entity.IRepository;
using Microsoft.AspNetCore.Http;
namespace MyBiking.MVC.Controllers
{
    public class RideController : Controller
    {
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IRideRepository _myBikingRepository;
        private readonly IMediator _mediator;

        public RideController(IRideRepository myBikingRepository, IMediator mediator)
        {
            
            this._myBikingRepository = myBikingRepository;
            this._mediator = mediator;
        }
        [Route("Ride/MonthlyRides/{month}")]
        [HttpGet]
        public async Task<ActionResult> MonthlyRides([FromRoute] string month, [FromQuery] string year)
        {
            if (month == null)
            {
                return RedirectToAction("index");
            }
            var result =await _mediator.Send(new RideQuery() { Month = month, Year = year });

            MonthlyRideModelView listRideViewModel = new MonthlyRideModelView()
            {
                RideDtos = result
            };

            if(listRideViewModel.Year==String.Empty || listRideViewModel.Month == String.Empty)
            {
                return RedirectToAction("index");
            }


            return View(listRideViewModel);
        }
        public async Task<ActionResult> Index(int? year)
        {
            var rideActivities = await _mediator.Send(new RideTimeActivityQuery() { Year = year });
            if (rideActivities == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (!year.HasValue)
            {
                return View(rideActivities);
            }

            return Ok(rideActivities.RideTimeActivitiesDates);
        }
        [HttpGet]
        [Route("Ride/Public")]
        public async Task<ActionResult> ShowPublicRides()
        {
            var response =await _mediator.Send(new PublicRidesQuery());
            return View("Public",response);
        }

        public async Task<ActionResult> Details(AgregatedRideQuery agregatedRideQuery)
        {
            var response =await _mediator.Send(agregatedRideQuery);
            return Ok(response);
        }

        public async Task<ActionResult> RideDetails(DetailsRideQuery detailsRideQuery)
        {
            var response = await _mediator.Send(detailsRideQuery);
            return Ok(response);
        }

        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login","Auth");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RideDtoCommand rideDtoCommand)
        {
            if(rideDtoCommand == null)
            {
                return BadRequest();
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(rideDtoCommand);
                }
                var status = await _mediator.Send(rideDtoCommand);

                if(status.Code == Entity.Enums.Code.HTTP500)
                {
                    return StatusCode(500, "Internal Server Error");
                }

                return RedirectToAction("Index", "Ride");
            }
            catch (Exception)
            {
                return View(rideDtoCommand);
            }

        }

        // POST: RideController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int? Id)
        {

            if (!Id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            Status result = await _mediator.Send(new DeleteRideCommand() { Id = Id.Value });
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
