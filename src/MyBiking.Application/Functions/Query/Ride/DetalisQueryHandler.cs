using MediatR;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    internal class DetalisQueryHandler : IRequestHandler<DetailsRideQuery, DetailsQueryViewModel>
    {

        private readonly IMyBikingRepository _myBikingRepository;

        public DetalisQueryHandler(IMyBikingRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<DetailsQueryViewModel> Handle(DetailsRideQuery request, CancellationToken cancellationToken)
        {
            var result =await _myBikingRepository.GetRideById(request.Id);
            if(result == null)
            {
                throw new ArgumentException($"There is no ride with id {request.Id}");
            }

            var ride = new DetailsQueryViewModel()
            {
                Wheelies = result.WheeleRides.Count(),
                WheelieMaxV = CalculateMaxwheelieV(result),
                //WheelieMaxV = Enumerable.Max(result.WheeleRides.SelectMany(w => w.WheeleItems?.ToList()).ToList(), w => w.Speed),
                TotalWheelieDistance = Enumerable.Sum(result.WheeleRides.ToList(), w => w.Distance),
            };

            return ride;
        }

        private double CalculateMaxwheelieV(Entity.Models.Ride ride)
        {
            var ridesContainingWheelieRidesList = ride.WheeleRides.SelectMany(w => w.WheeleItems);
            if (ridesContainingWheelieRidesList.Any(wi => wi.Speed != null))
            {
                return Enumerable.Max(ridesContainingWheelieRidesList, w => w.Speed);
            }
            return 0d;
        }
    }
}
