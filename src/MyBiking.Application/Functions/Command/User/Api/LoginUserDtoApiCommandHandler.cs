using AutoMapper;
using MediatR;
using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Command.User.Api
{
    internal class LoginUserDtoApiCommandHandler : IRequestHandler<LoginUserDtoApiCommand, Status>
    {
        private readonly IMapper _mapper;
        private readonly IMyBikingRepository _myBikingRepository;

        public LoginUserDtoApiCommandHandler(IMapper mapper, IMyBikingRepository myBikingRepository)
        {
            this._mapper = mapper;
            this._myBikingRepository = myBikingRepository;
        }

        public async Task<Status> Handle(LoginUserDtoApiCommand request, CancellationToken cancellationToken)
        {
            var mappesUser = _mapper.Map<ApplicationUser>(request);
            var result =await _myBikingRepository.LoginApi(mappesUser);
            return result;
        }
    }
}
