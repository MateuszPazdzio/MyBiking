using AutoMapper;
using FluentValidation;
using MyBiking.Application.Models;
using MyBikingApi.Models.Dtos;

namespace MyBiking.Application.Mapper
{
    public class RideDtoProfile : Profile
    {
        public RideDtoProfile()
        {
            CreateMap<RideDto, Ride>();
            CreateMap<PointDto, Point>();
            CreateMap<WheelieRideDto, WheelieRide>();
            CreateMap<WheelieItemDto, WheelieItem>()
                .ForMember(m => m.WheelePoint, cfg => cfg.MapFrom(src => new WheeliePoint()
                {
                    Address = src.Address,
                    Latitude = src.Latitude,
                    Longitude = src.Longitude
                }));
        }
    }
}
