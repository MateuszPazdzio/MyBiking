using MediatR;
using MyBiking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    public class RideQuery : IRequest<List<RideDto>>
    {
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
