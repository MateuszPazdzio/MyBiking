using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Query.Ride
{
    public class PublicRidesQueryViewModel
    {
        public List<RideDto> Rides { get; set; }
    }
}
