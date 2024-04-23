using MediatR;
using MyBiking.Application.Dtos;
using MyBiking.Entity.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Command.Wheelie
{
    internal class DeleteWheelieCommandHandler : IRequestHandler<DeleteWheelieCommand, Status>
    {
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IWheelieRideRepository _myBikingRepository;

        public DeleteWheelieCommandHandler(IWheelieRideRepository myBikingRepository)
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
