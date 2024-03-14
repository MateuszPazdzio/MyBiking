using AutoMapper;
using MediatR;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBiking.Entity.Models;
using MyBiking.Application.Dtos;

namespace MyBiking.Application.Functions.Command.RideApi
{
    internal class RideDtoApiCommandHandler : IRequestHandler<RideDtoApiCommand,Status>
    {
        private readonly IMapper _mapper;
        private readonly IMyBikingRepository _myBikingRepository;

        public RideDtoApiCommandHandler(IMapper mapper, IMyBikingRepository myBikingRepository)
        {
            this._mapper = mapper;
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<Status> Handle(RideDtoApiCommand request, CancellationToken cancellationToken)
        {
            var ride = _mapper.Map<Entity.Models.Ride>(request);
            Status status =await _myBikingRepository.CreateRide(ride);
            return status;
        }
    }
}
