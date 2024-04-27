using AutoMapper;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.RideApi;
using MyBiking.Application.Functions.Command.User;
using MyBiking.Application.Functions.Command.User.Api;
using MyBiking.Entity.IRepository;
using MyBiking.Entity.Models;
using MyBikingApi.Models.Dtos;

namespace MyBiking.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        private readonly IUserHttpContext _userHttpContext;

        public AutoMapperProfile(IUserHttpContext userHttpContext)
        {
            this._userHttpContext = userHttpContext;

            CreateMap<PointDto, Point>()
                .ReverseMap();
                
            CreateMap<WheelieRideDto, WheelieRide>()
                .ReverseMap();

            CreateMap<WheelieItemDto, WheelieItem>()
                .ForMember(m => m.WheelePoint, cfg => cfg.MapFrom(src => new WheeliePoint()
                {
                    Address = src.Address,
                    Latitude = src.Latitude,
                    Longitude = src.Longitude
                }))
                .ReverseMap();

            CreateMap<RegisterUserDto, ApplicationUser>();

            CreateMap<LoginUserDto, ApplicationUser>();

            CreateMap<LoginUserDtoApiCommand, ApplicationUser>();

            CreateMap<RegisterUserDtoCommand, ApplicationUser>();

            CreateMap<LoginUserDtoCommand, ApplicationUser>();

            CreateMap<RideDto, Entity.Models.Ride>()
                .ForMember(m => m.ApplicationUserId, cfg => cfg.MapFrom(opt => _userHttpContext.GetUser().Id));

            CreateMap<Entity.Models.Ride, RideDto>();

            CreateMap<RideDtoApi, Entity.Models.Ride>()
                .ForMember(m => m.ApplicationUserId, cfg => cfg.MapFrom(opt => _userHttpContext.GetUser().Id))
                .ReverseMap();

            CreateMap<RideDtoApiCommand, Entity.Models.Ride>()
                .ForMember(m => m.ApplicationUserId, cfg => cfg.MapFrom(opt => _userHttpContext.GetUser().Id));
        }
    }
}
