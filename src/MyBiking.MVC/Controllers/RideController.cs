using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Entity.Models;
using MyBiking.Application.Functions.Query.Ride;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command;
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

        // GET: RideController
        public async Task<ActionResult> Index()
        {
            //zwrócić listę obiektów RideTimeActivity
            //
            RideModelView rideModelView = new RideModelView()
            {
                RideActivities = await _mediator.Send(new RideTimeActivityQuery())
            };
        

            return View(rideModelView);

        //var results =await _mediator.Send(new RideTimeActivityQuery());
        //return View(results);
    }

        // GET: RideController/Details/5
        [Route("RideController/Details")]
        public async Task<ActionResult> Details(AgregatedRideQuery agregatedRideQuery)
        {
            var response =await _mediator.Send(agregatedRideQuery);
            return View(response);
        }

        // GET: RideController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RideController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RideDtoCommand rideDtoCommand)
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
