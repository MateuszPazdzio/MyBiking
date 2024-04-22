using MediatR;
using MyBiking.Application.ViewModels;
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
            var monthlyAgregatedRideResponse= new MonthlyAgregatedRideResponse()
            {
                Distance = rides.Sum(r => r.Distance),
                Wheelies = rides.Sum(r => r.WheeleRides?.Count),
                WheelieMaxV = CalculateWheelieVMax(rides),
                Rides = rides.Count(),
                TotalWheelieDistance = Enumerable.Sum(rides.SelectMany(r => r.WheeleRides).ToList(), w => w.Distance),
            };

            return monthlyAgregatedRideResponse;
        }
        private double? CalculateWheelieVMax(List<Entity.Models.Ride> rides)
        {
            var wheelieItems = rides.SelectMany(r => r.WheeleRides?.SelectMany(w => w.WheeleItems?.ToList())).ToList();
            var wheelieItemsAddedByApi = wheelieItems.Where(wi=>wi.Speed!=null).ToList();
            if(wheelieItemsAddedByApi.Any())
            {
                return Enumerable.Max(wheelieItemsAddedByApi,w=>w.Speed);
            }
            return null;
            
        }
    }
}
