using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using MyBiking.Application.Dtos;
using MyBiking.Entity.IRepository;

namespace MyBiking.Application.Functions.Command.RideApi
{
    internal class RideDtoApiCommandHandler : IRequestHandler<RideDtoApiCommand,Status>
    {
        private readonly IMapper _mapper;
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IRideRepository _myBikingRepository;

        public RideDtoApiCommandHandler(IMapper mapper, IRideRepository myBikingRepository)
        {
            this._mapper = mapper;
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<Status> Handle(RideDtoApiCommand request, CancellationToken cancellationToken)
        {
            request.Creation_Date = DateTime.Now;
            var ride = _mapper.Map<Entity.Models.Ride>(request);
            Status status =await _myBikingRepository.CreateRide(ride);
            return status;
        }
    }
}
