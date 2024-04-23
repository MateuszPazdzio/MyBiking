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
    internal class RideQueryHandler : IRequestHandler<RideQuery, List<RideDto>>
    {
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IRideRepository _myBikingRepository;
        private readonly IMapper _mapper;

        public RideQueryHandler(IRideRepository myBikingRepository, IMapper mapper)
        {
            this._myBikingRepository = myBikingRepository;
            this._mapper = mapper;
        }
        public async Task<List<RideDto>> Handle(RideQuery request, CancellationToken cancellationToken)
        {
            var result =await _myBikingRepository.GetRidesByMonthAsync(request.Year,request.Month);
            if(result == null)
            {
                throw new ArgumentException("Theres is not such ride, that occured in a month of specific year");
            }

            List<RideDto> rideDtos = _mapper.Map<List<RideDto>>(result);

            return rideDtos;
        }
    }
}
