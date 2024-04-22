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
    internal class WheelieRideQueryHandler : IRequestHandler<WheelieRideQuery, WheelieRideViewModel>
    {
        private readonly IMyBikingRepository _myBikingRepository;
        private readonly IMapper mapper;

        public WheelieRideQueryHandler(IMyBikingRepository myBikingRepository, IMapper mapper)
        {
            this._myBikingRepository = myBikingRepository;
            this.mapper = mapper;
        }
        public async Task<WheelieRideViewModel> Handle(WheelieRideQuery request, CancellationToken cancellationToken)
        {
            var result =await _myBikingRepository.GetWheelieRideById(request.WheelieRideId);

            var response = new WheelieRideViewModel()
            {
                Addrees = CalculateWheelieRideParameter(result, Entity.Enums.WheelieRide.Addrees),
                Altitude = CalculateWheelieRideParameter(result, Entity.Enums.WheelieRide.Altitude),
                RotateX = CalculateWheelieRideParameter(result, Entity.Enums.WheelieRide.RotateX),
                VMax = CalculateWheelieRideParameter(result, Entity.Enums.WheelieRide.VMax),
            };

            return response;
        }

        private string CalculateWheelieRideParameter(Entity.Models.WheelieRide wheelieRide, Entity.Enums.WheelieRide paramType)
        {
            Dictionary<Entity.Enums.WheelieRide, Func<WheelieItem, string>> wheelieItemParams =
                new Dictionary<Entity.Enums.WheelieRide, Func<WheelieItem, string>>()
            {
                {Entity.Enums.WheelieRide.Altitude, wi => wi.Altitude.ToString()},
                {Entity.Enums.WheelieRide.RotateX, wi=>wi.MaxRotateX.ToString() },
                {Entity.Enums.WheelieRide.VMax, wi=>wi.Speed.ToString()},
                {Entity.Enums.WheelieRide.Addrees, wi=>wi.Address}
            };

            if (wheelieRide == null)
            {
                return "";
            }

            if (wheelieRide.WheeleItems.Any(wi => wheelieItemParams[paramType].Invoke(wi) != null))
            {
                if (paramType == Entity.Enums.WheelieRide.Addrees)
                {
                    return wheelieRide.WheeleItems.FirstOrDefault(wi => wi.Address != null).Address;
                }
                return Enumerable.Max(wheelieRide.WheeleItems, wi => wheelieItemParams[paramType].Invoke(wi)).ToString();
            }
            return "";

        }
    }
}
