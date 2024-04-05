using MediatR;
using MyBiking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Command.User.Api
{
    public class LoginUserDtoApiCommand : LoginUserDtoApi, IRequest<Status>
    {
    }
}
