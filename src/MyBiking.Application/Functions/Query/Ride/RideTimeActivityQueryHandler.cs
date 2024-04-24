using MediatR;
using MyBiking.Application.Ride;
using MyBiking.Entity.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    internal class RideTimeActivityQueryHandler : IRequestHandler<RideTimeActivityQuery, RideTimeActivity>
    {
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IRideRepository _myBikingRepository;

        public RideTimeActivityQueryHandler(IRideRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<RideTimeActivity> Handle(RideTimeActivityQuery request, CancellationToken cancellationToken)
        {
            List<Entity.Models.Ride> rides = await _myBikingRepository.GetRideActivitiesSelectedByYear(request.Year);
            List<RideTimeActivity> rideTimeActivities = new List<RideTimeActivity>();

            RideTimeActivity rideTimeActivity = new RideTimeActivity();

            if(!request.Year.HasValue)
            {
                var years = rides.DistinctBy(r=>r.StartingDateTime.Year)
                    .Select(rd=>rd.StartingDateTime.Year)
                    .OrderByDescending(rd=>rd)
                    .ToList();
                if(years.Count() == 0)
                {
                    rideTimeActivity.RideTimeActivitiesDates = GetDistinctRideActivitiesByMonth(rides, null);
                    return rideTimeActivity;
                }
                rideTimeActivity.Years = years;
                var latestYear = rideTimeActivity.Years.Max();

                rideTimeActivity.RideTimeActivitiesDates = GetDistinctRideActivitiesByMonth(rides, latestYear);

                return rideTimeActivity;
            }

            rideTimeActivity.RideTimeActivitiesDates = GetDistinctRideActivitiesByMonth(rides, request.Year);

            return rideTimeActivity;
        }

        private List<DateTime> GetDistinctRideActivitiesByMonth(List<Entity.Models.Ride> rides, int? latestYear)
        {
            if (!latestYear.HasValue)
            {
                return new List<DateTime>();
            }

            return rides.Where(r => r.StartingDateTime.Year == latestYear)
                   .Select(r => r.StartingDateTime)
                   .DistinctBy(r => r.ToString("MMMM"))
                   .OrderBy(r => r, new RideTimeActivityDatesComparer())
                   .ToList();
        }
    }

    internal class RideTimeActivityDatesComparer : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return x.CompareTo(y);
        }
    }
}
