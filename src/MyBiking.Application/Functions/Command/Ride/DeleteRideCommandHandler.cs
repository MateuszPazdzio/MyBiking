using MediatR;
using MyBiking.Application.Dtos;
using MyBiking.Entity.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Command.Ride
{
    internal class DeleteRideCommandHandler : IRequestHandler<DeleteRideCommand, Status>
    {
        //private readonly IMyBikingRepository _myBikingRepository;
        private readonly IRideRepository _myBikingRepository;

        public DeleteRideCommandHandler(IRideRepository myBikingRepository)
        {
            this._myBikingRepository = myBikingRepository;
        }
        public async Task<Status> Handle(DeleteRideCommand request, CancellationToken cancellationToken)
        {
            var status =await _myBikingRepository.DeleteRide(request.Id);
            return status;
        }
    }
}
