using MediatR;
using MyBikingApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyBiking.Entity.Models;
using AutoMapper;

namespace MyBiking.Application.Functions.Query.Wheelie
{
    public class WheelieQueryHandler : MediatR.IRequestHandler<WheelieRidesQuery, List<WheelieRideDto>>
    {
        private readonly IMyBikingRepository _myBikingRepository;
        private readonly IMapper mapper;

        public WheelieQueryHandler(IMyBikingRepository myBikingRepository, IMapper mapper)
        {
            this._myBikingRepository = myBikingRepository;
            this.mapper = mapper;
        }
        public async Task<List<WheelieRideDto>> Handle(WheelieRidesQuery request, CancellationToken cancellationToken)
        {
            List<WheelieRide> wheelieRides = await _myBikingRepository.GetWheelieRidesById(request.RideId);
            if(wheelieRides == null)
            {
                return null;
            }
            var mappedWheelieRides = mapper.Map<List<WheelieRideDto>>(wheelieRides);
            return mappedWheelieRides;
        }
    }
}
