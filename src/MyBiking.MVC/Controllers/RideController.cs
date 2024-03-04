using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Entity.Models;
using MyBiking.Application.Functions.Query.Ride;

using MyBiking.Application.Functions.Query.Ride;

namespace MyBiking.MVC.Controllers
{
    public class RideController : Controller
    {
        private readonly IMyBikingRepository myBikingRepository;
        private readonly IMediator mediator;

        public RideController(IMyBikingRepository myBikingRepository, IMediator mediator)
        {
            
            this.myBikingRepository = myBikingRepository;
            this.mediator = mediator;
        }

        // GET: RideController
        public ActionResult Index()
        {
            //zwrócić listę obiektów RideTimeActivity
            //
            var results = mediator.Send(new RideTimeActivityQuery());
            return View(results);
        }

        // GET: RideController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RideController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RideController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
