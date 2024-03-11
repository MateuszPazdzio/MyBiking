using AutoMapper;
using MediatR;
using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Command.User
{
    internal class LoginUserDtoCommandHandler : IRequestHandler<LoginUserDtoCommand, Status>
    {
        private IMapper _mapper;
        private IMyBikingRepository _myBikingRepository;

        public LoginUserDtoCommandHandler(IMapper mapper, IMyBikingRepository myBikingRepository)
        {
            _mapper = mapper;
            _myBikingRepository = myBikingRepository;
        }
        public Task<Status> Handle(LoginUserDtoCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request);
            return _myBikingRepository.LoginUser(user);
        }
    }
}
