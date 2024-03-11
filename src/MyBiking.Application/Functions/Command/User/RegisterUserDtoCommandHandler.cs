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
    internal class RegisterUserDtoCommandHandler : IRequestHandler<RegisterUserDtoCommand, Status>
    {
        private readonly IMapper _mapper;
        private readonly IMyBikingRepository _myBikingRepository;

        public RegisterUserDtoCommandHandler(IMapper mapper, IMyBikingRepository myBikingRepository)
        {
            _mapper = mapper;
            _myBikingRepository = myBikingRepository;
        }

        public async Task<Status> Handle(RegisterUserDtoCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _myBikingRepository.CreateUser(user);

            return result;
        }

    }
}
