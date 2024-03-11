using MediatR;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    internal class AgregatedRideQueryHandler : IRequestHandler<AgregatedRideQuery, MonthlyAgregatedRideResponse>
    {
        private readonly IMyBikingRepository _myBikingRepository;

        public AgregatedRideQueryHandler(IMyBikingRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<MonthlyAgregatedRideResponse> Handle(AgregatedRideQuery request, CancellationToken cancellationToken)
        {
            List<Entity.Models.Ride> rides = await _myBikingRepository.GetRidesByMonthAsync(request.Year,request.Month);
            return new MonthlyAgregatedRideResponse()
            {
                //Distance = rides.Sum(r=>r.Points)
                //jezeli da się to zliczyć w aplikacji to zrobic to tą drogą, jak nie to policzyc po Pointach

                Wheelies = rides.Sum(r => r.WheeleRides.Count),
                WheelieMaxV = Enumerable.Max(rides.SelectMany(r => r.WheeleRides.SelectMany(w => w.WheeleItems.ToList())).ToList(), w => w.Speed),
                Rides = rides.Count(),
                TotalWheelieDistance = Enumerable.Sum(rides.SelectMany(r => r.WheeleRides).ToList(), w => w.Distnace),
            };
            //monthlyAgregatedRideResponse
        }
    }
}
