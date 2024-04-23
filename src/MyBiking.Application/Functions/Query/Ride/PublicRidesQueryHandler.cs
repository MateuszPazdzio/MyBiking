using AutoMapper;
using MediatR;
using MyBiking.Application.Dtos;
using MyBiking.Application.ViewModels;
using MyBiking.Entity.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    internal class PublicRidesQueryHandler : IRequestHandler<PublicRidesQuery, PublicRidesQueryViewModel>
    {
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IRideRepository _myBikingRepository;
        private readonly IMapper _mapper;

        public PublicRidesQueryHandler(IRideRepository myBikingRepository, IMapper mapper)
        {
            this._myBikingRepository = myBikingRepository;
            this._mapper = mapper;
        }
        public async Task<PublicRidesQueryViewModel> Handle(PublicRidesQuery request, CancellationToken cancellationToken)
        {
            var publicRides =await _myBikingRepository.GetPublicRides();
            var mappedPublicRides = _mapper.Map<List<RideDto>>(publicRides);

            var response = new PublicRidesQueryViewModel()
            {
                Rides = mappedPublicRides,
            };

            return response;
        }
    }
}
