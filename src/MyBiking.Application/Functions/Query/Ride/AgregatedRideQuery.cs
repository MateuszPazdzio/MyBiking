using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    public class AgregatedRideQuery : IRequest<MonthlyAgregatedRideResponse>
    {
        public string Year { get; set; }
        public string Month { get; set; }
    }
}
