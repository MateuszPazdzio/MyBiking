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

        public WheelieRideQueryHandler(IMyBikingRepository myBikingRepository, IMapper mapper)
        {
            this._myBikingRepository = myBikingRepository;
            this.mapper = mapper;
        }
        public async Task<WheelieRideResponse> Handle(WheelieRideQuery request, CancellationToken cancellationToken)
        {
            var result =await _myBikingRepository.GetWheelieRideById(request.WheelieRideId);

            var response = new WheelieRideResponse()
            {
                Addrees1 = result.WheeleItems.FirstOrDefault(wi => !String.IsNullOrEmpty(wi.Address)).Address,
                Addrees2 = result.WheeleItems.FirstOrDefault(wi => !String.IsNullOrEmpty(wi.Address)).Address,
                RotateX = result.WheeleItems.Max(wi => wi.MaxRotateX).ToString(),
                VMax = result.WheeleItems.Max(wi => wi.Speed).ToString(),
            };

            return response;
        }
    }
}
