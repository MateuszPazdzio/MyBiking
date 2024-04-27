using MediatR;
using MyBiking.Application.Interfaces;
using MyBiking.Application.Ride;
using MyBiking.Entity.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    public class RideTimeActivityQueryHandler : IRequestHandler<RideTimeActivityQuery, RideTimeActivity>
    {
        private readonly IRideRepository _myBikingRepository;
        private readonly IDistictRideActivitiesSupplier _distictRideActivitiesSupplier;

        public RideTimeActivityQueryHandler(IRideRepository myBikingRepository,
            IDistictRideActivitiesSupplier distictRideActivitiesSupplier)
        {
            this._myBikingRepository = myBikingRepository;
            this._distictRideActivitiesSupplier = distictRideActivitiesSupplier;
        }

        public async Task<RideTimeActivity>? Handle(RideTimeActivityQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return null;
            }
            var rideActivities = await _myBikingRepository.GetRideActivitiesSelectedByYear(request.Year);

            if (rideActivities == null)
            {
                return null;
            }

            List<RideTimeActivity> rideTimeActivities = new List<RideTimeActivity>();

            RideTimeActivity rideTimeActivity = new RideTimeActivity();

            if(!request.Year.HasValue)
            {
                var years = rideActivities.DistinctBy(r=>r.StartingDateTime.Year)
                    .Select(rd=>rd.StartingDateTime.Year)
                    .OrderByDescending(rd=>rd)
                    .ToList();
                if(years.Count() == 0)
                {
                    rideTimeActivity.RideTimeActivitiesDates =
                        _distictRideActivitiesSupplier.GetDistinctRideActivitiesByMonth(rideActivities, null);
                    return rideTimeActivity;
                }
                rideTimeActivity.Years = years;
                var latestYear = rideTimeActivity.Years.Max();

                rideTimeActivity.RideTimeActivitiesDates = 
                    _distictRideActivitiesSupplier.GetDistinctRideActivitiesByMonth(rideActivities, latestYear);

                return rideTimeActivity;
            }

            rideTimeActivity.RideTimeActivitiesDates = 
                _distictRideActivitiesSupplier.GetDistinctRideActivitiesByMonth(rideActivities, request.Year);

            return rideTimeActivity;
        }
    }
}
