using MediatR;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    internal class DetalisQueryHandler : IRequestHandler<DetailsRideQuery, DetailsQueryResponse>
    {

        private readonly IMyBikingRepository _myBikingRepository;

        public DetalisQueryHandler(IMyBikingRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<DetailsQueryResponse> Handle(DetailsRideQuery request, CancellationToken cancellationToken)
        {
            var result =await _myBikingRepository.GetRideById(request.Id);
            if(result == null)
            {
                throw new ArgumentException($"There is no ride with id {request.Id}");
            }

            var ride = new DetailsQueryResponse()
            {
                Wheelies = result.WheeleRides.Count(),
                WheelieMaxV = Enumerable.Max(result.WheeleRides.SelectMany(w => w.WheeleItems.ToList()).ToList(), w => w.Speed),
                TotalWheelieDistance = Enumerable.Sum(result.WheeleRides.ToList(), w => w.Distance),
            };

            return ride;
        }
    }
}
