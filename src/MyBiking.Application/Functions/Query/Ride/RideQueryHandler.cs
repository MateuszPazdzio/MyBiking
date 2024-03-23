using MediatR;
using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    internal class RideQueryHandler : IRequestHandler<RideQuery, List<RideDto>>
    {
        private readonly IMyBikingRepository _myBikingRepository;

        public RideQueryHandler(IMyBikingRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }
        public async Task<List<RideDto>> Handle(RideQuery request, CancellationToken cancellationToken)
        {
            //_myBikingRepository.GetRidesByMonthAsync();
            throw new NotImplementedException();
        }
    }
}
