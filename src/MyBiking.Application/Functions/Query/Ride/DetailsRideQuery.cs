using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    public class DetailsRideQuery : IRequest<DetailsQueryResponse>
    {
        public int Id { get; set; } 
    }
}
