using AutoMapper;
using MediatR;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBiking.Entity.Enums;

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
                //Addrees = result.WheeleItems?.FirstOrDefault(wi => !String.IsNullOrEmpty(wi.Address)).Address,
                Addrees = CalculateWheelieRideParameter(result, Entity.Enums.WheelieRide.Addrees),
                //Altitude = Enumerable.Max(result.WheeleItems?.Select(w=>w.Altitude).ToList()).ToString(),
                Altitude = CalculateWheelieRideParameter(result, Entity.Enums.WheelieRide.Altitude),
                //RotateX = result.WheeleItems?.Max(wi => wi.MaxRotateX).ToString(),
                RotateX = CalculateWheelieRideParameter(result, Entity.Enums.WheelieRide.RotateX),
                //VMax = result.WheeleItems?.Max(wi => wi.Speed).ToString(),
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

            if (wheelieRide.WheeleItems.Any(wi=>wheelieItemParams[paramType].Invoke(wi)!=null))
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
