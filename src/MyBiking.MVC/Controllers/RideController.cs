using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Entity.Models;
using MyBiking.Application.Functions.Query.Ride;
using MyBiking.Application.Functions.Command.RideApi;
using MyBiking.Application.Dtos;
using MyBiking.Application.ViewModels;
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
                throw new Exception("Year does not exists");
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RideDtoApiCommand rideDtoCommand)
        {
            //usunąć user id
            //if (!ModelState.IsValid)
            //{
            //    return View(rideDtoCommand);
            //}
           var status = await _mediator.Send(rideDtoCommand);
            return RedirectToAction("Index", "Ride");
        }

        // GET: RideController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RideController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RideController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RideController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
