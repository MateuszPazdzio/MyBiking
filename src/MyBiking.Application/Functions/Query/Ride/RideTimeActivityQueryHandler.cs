using MediatR;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    internal class RideTimeActivityQueryHandler : IRequestHandler<RideTimeActivityQuery, List<RideTimeActivity>>
    {
        private readonly IMyBikingRepository _myBikingRepository;

        public RideTimeActivityQueryHandler(IMyBikingRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<List<RideTimeActivity>> Handle(RideTimeActivityQuery request, CancellationToken cancellationToken)
        {
            return await _myBikingRepository.GetTimeOfRideActivities();
        }
        //public async Task<Dictionary<int, HashSet<string>>> Handle(RideTimeActivityQuery request, CancellationToken cancellationToken)
        //{
        //    return await _myBikingRepository.GetTimeOfRideActivities();
        //}


    }
}
