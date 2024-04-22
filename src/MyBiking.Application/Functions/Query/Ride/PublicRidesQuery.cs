using MediatR;
using MyBiking.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    public class PublicRidesQuery : IRequest<PublicRidesQueryViewModel>
    {

    }
}
