using AutoMapper;
using FluentValidation;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.Ride;
using MyBiking.Application.Functions.Command.RideApi;
using MyBiking.Application.Functions.Command.User;
using MyBiking.Application.Functions.Command.User.Api;
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

            //CreateMap<RideDto, Ride>()
            //    .ForMember(m => m.ApplicationUser, cfg => cfg.MapFrom(opt => _userHttpContext.GetUser()));

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
            //    .ForMember(u => u.NationalityId, cfg => cfg.MapFrom(src => Convert.ToInt32(src.Nationality)))
            //    .ForMember(u => u.Nationality, cfg => cfg.MapFrom(src=>new Nationality() { Id = Convert.ToInt32(src.Nationality)}));

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
