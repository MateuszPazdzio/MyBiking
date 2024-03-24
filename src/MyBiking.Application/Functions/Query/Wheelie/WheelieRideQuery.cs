using MediatR;
using MyBikingApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Wheelie
{
    public class WheelieRideQuery : IRequest<List<WheelieRideDto>>
    {
        public int? RideId { get; set; }
    }
}
