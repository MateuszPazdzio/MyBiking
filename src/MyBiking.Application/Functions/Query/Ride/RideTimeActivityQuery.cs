using MediatR;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    //public class RideTimeActivityQuery : RideTimeActivity, IRequest<Dictionary<int, HashSet<string>>>
    public class RideTimeActivityQuery : IRequest<List<RideTimeActivity>>
    {
        
    }
}
