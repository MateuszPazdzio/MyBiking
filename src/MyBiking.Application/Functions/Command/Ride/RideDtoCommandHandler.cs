using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.RideApi;
using MyBiking.Entity.IRepository;

namespace MyBiking.Application.Functions.Command.Ride
{
    internal class RideDtoCommandHandler : IRequestHandler<RideDtoCommand,Status>
    {
        private readonly IMapper _mapper;
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IRideRepository _myBikingRepository;

        public RideDtoCommandHandler(IMapper mapper, IRideRepository myBikingRepository)
        {
            this._mapper = mapper;
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<Status> Handle(RideDtoCommand request, CancellationToken cancellationToken)
        {
            var ride = _mapper.Map<Entity.Models.Ride>(request);
            ride.Creation_Date = DateTime.Now;
            Status status =await _myBikingRepository.CreateRide(ride);
            return status;
        }
    }
}
