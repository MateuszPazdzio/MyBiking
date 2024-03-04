using AutoMapper;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.User;
using MyBiking.Application.Models;

namespace MyBiking.Application.Mapper
{
    public class AuthUserDtoProfile : Profile
    {
        public AuthUserDtoProfile()
        {
            CreateMap<RegisterUserDto, ApplicationUser>();
            //    .ForMember(u => u.NationalityId, cfg => cfg.MapFrom(src => Convert.ToInt32(src.Nationality)))
            //    .ForMember(u => u.Nationality, cfg => cfg.MapFrom(src=>new Nationality() { Id = Convert.ToInt32(src.Nationality)}));

            CreateMap<LoginUserDto, ApplicationUser>();

            CreateMap<RegisterUserDtoCommand, ApplicationUser>();
            CreateMap<LoginUserDtoCommand, ApplicationUser>();
            //CreateMap<LoginUserDto, ApplicationUser>().
            //    ForMember(u => u.PasswordHelpers, cfg => cfg.MapFrom(src => new PasswordHelpers() { Password = src.Password }));

            //CreateMap<RegisterUserDto, ApplicationUser>()
            //    .ForMember(u => u.PasswordHelpers, cfg => cfg.MapFrom(src => new PasswordHelpers() { Password = src.Password }))
            //    .ForMember(u => u.CreationDate, cfg => cfg.MapFrom(src => DateTime.Today));
        }
    }
}
