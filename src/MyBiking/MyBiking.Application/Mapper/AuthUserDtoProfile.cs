using AutoMapper;
using MyBiking.Application.Models;

namespace MyBiking.Application.Mapper
{
    public class AuthUserDtoProfile : Profile
    {
        public AuthUserDtoProfile()
        {
            CreateMap<LoginUserDto, User>().
                ForMember(u => u.PasswordHelpers, cfg => cfg.MapFrom(src => new PasswordHelpers() { Password = src.Password }));

            CreateMap<RegisterUserDto, User>()
                .ForMember(u => u.PasswordHelpers, cfg => cfg.MapFrom(src => new PasswordHelpers() { Password = src.Password }))
                .ForMember(u => u.CreationDate, cfg => cfg.MapFrom(src => DateTime.Today));
        }
    }
}
