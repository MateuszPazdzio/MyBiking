using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Entity.Models;
using MyBiking.Application.Functions.Query.Ride;
using MyBiking.Application.Functions.Command.RideApi;
using MyBiking.Application.Dtos;
using MyBiking.Application.ViewModels;
using MyBiking.Application.Functions.Command.Ride;
namespace MyBiking.MVC.Controllers
{
    public class RideController : Controller
    {
        private readonly IMyBikingRepository _myBikingRepository;
        private readonly IMediator _mediator;

        public RideController(IMyBikingRepository myBikingRepository, IMediator mediator)
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

            if(listRideViewModel.Year==String.Empty)
            {
                return RedirectToAction("index");
                //throw new Exception("Year does not exists");
            }

            if(listRideViewModel.Month == String.Empty)
            {
                throw new Exception("Month does not exists");
            }


            return View(listRideViewModel);
        }
        public async Task<ActionResult> Index(int? year)
        {
            var RideActivities = await _mediator.Send(new RideTimeActivityQuery() { Year = year });
            if (!year.HasValue)
            {
                return View(RideActivities);
            }

            return Ok(RideActivities.RideTimeActivitiesDates);
        }
        [HttpGet]
        [Route("Ride/Public")]
        public async Task<ActionResult> ShowPublicRides()
        {
            var response =await _mediator.Send(new PublicRidesQuery());
            return View("Public",response);
        }
        // GET: RideController/Details/5
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

        // GET: RideController/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login","Auth");
            }
            return View();
        }

        // POST: RideController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(RideDtoApiCommand rideDtoCommand)
        public async Task<ActionResult> Create(RideDtoCommand rideDtoCommand)
        {
            //usunąć user id
            if (!ModelState.IsValid)
            {
                return View(rideDtoCommand);
            }
            var status = await _mediator.Send(rideDtoCommand);
            return RedirectToAction("Index", "Ride");
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
