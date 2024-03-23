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

        public RideTimeActivityQueryHandler(IMyBikingRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<DetailsQueryResponse> Handle(DetailsRideQuery request, CancellationToken cancellationToken)
        {
            var result =await _myBikingRepository.GetRideById(request.Id);
        }
    }
}
