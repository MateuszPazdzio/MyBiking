using MediatR;
using MyBiking.Application.Dtos;

namespace MyBiking.Application.Functions.Command.User
{
    public class RegisterUserDtoCommand : RegisterUserDto, IRequest<Status>
    {
    }
}