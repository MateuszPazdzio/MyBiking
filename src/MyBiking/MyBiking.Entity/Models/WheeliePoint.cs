using Microsoft.EntityFrameworkCore;

namespace MyBiking.Application.Models
{
    public class WheeliePoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }
}
