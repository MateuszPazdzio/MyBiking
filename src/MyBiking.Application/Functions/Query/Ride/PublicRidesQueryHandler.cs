﻿using AutoMapper;
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
    internal class PublicRidesQueryHandler : IRequestHandler<PublicRidesQuery, PublicRidesQueryResponse>
    {
        private readonly IMyBikingRepository _myBikingRepository;
        private readonly IMapper _mapper;

        public PublicRidesQueryHandler(IMyBikingRepository myBikingRepository, IMapper mapper)
        {
            this._myBikingRepository = myBikingRepository;
            this._mapper = mapper;
        }
        public async Task<PublicRidesQueryResponse> Handle(PublicRidesQuery request, CancellationToken cancellationToken)
        {
            var publicRides =await _myBikingRepository.GetPublicRides();
            var mappedPublicRides = _mapper.Map<List<RideDto>>(publicRides);

            var response = new PublicRidesQueryResponse()
            {
                Rides = mappedPublicRides,
            };

            return response;
        }
    }
}