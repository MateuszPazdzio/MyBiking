using AutoMapper;
using MediatR;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Wheelie
{
    internal class WheelieRideQueryHandler : IRequestHandler<WheelieRideQuery, WheelieRideResponse>
    {
        private readonly IMyBikingRepository _myBikingRepository;
        private readonly IMapper mapper;

        public WheelieQueryHandler(IMyBikingRepository myBikingRepository, IMapper mapper)
        {
            this._myBikingRepository = myBikingRepository;
            this.mapper = mapper;
        }
        public Task<WheelieRideResponse> Handle(WheelieRideQuery request, CancellationToken cancellationToken)
        {
            var result = _myBikingRepository.GetWheelieRideById(request);
        }
    }
}
