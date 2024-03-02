using AutoMapper;
using MyBiking.Application.Dtos;
using MyBiking.Application.Models;

namespace MyBiking.Application.Mapper
{
    public class AuthUserDtoProfile : Profile
    {
        public AuthUserDtoProfile()
        {
            CreateMap<RegisterUserDto, ApplicationUser>();
            CreateMap<LoginUserDto, ApplicationUser>();
            //CreateMap<LoginUserDto, ApplicationUser>().
            //    ForMember(u => u.PasswordHelpers, cfg => cfg.MapFrom(src => new PasswordHelpers() { Password = src.Password }));

            //CreateMap<RegisterUserDto, ApplicationUser>()
            //    .ForMember(u => u.PasswordHelpers, cfg => cfg.MapFrom(src => new PasswordHelpers() { Password = src.Password }))
            //    .ForMember(u => u.CreationDate, cfg => cfg.MapFrom(src => DateTime.Today));
        }
    }
}
