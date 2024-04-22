using MediatR;
using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Command.Wheelie
{
    internal class DeleteWheelieCommandHandler : IRequestHandler<DeleteWheelieCommand, Status>
    {
        private readonly IMyBikingRepository _myBikingRepository;

        public DeleteWheelieCommandHandler(IMyBikingRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }
        public async Task<Status> Handle(DeleteWheelieCommand request, CancellationToken cancellationToken)
        {
            var status =await _myBikingRepository.DeleteWheelie(request.Id);
            return status;
        }
    }
}
