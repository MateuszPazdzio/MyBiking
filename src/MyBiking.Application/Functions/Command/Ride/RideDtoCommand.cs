using MediatR;
using MyBiking.Application.Dtos;
using MyBikingApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Command.Ride
{
    public class RideDtoCommand: RideDto, IRequest<Status>
    {
    }
}
