using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.ViewModels
{
    public class PublicRidesQueryViewModel
    {
        public List<RideDto> Rides { get; set; }
    }
}
