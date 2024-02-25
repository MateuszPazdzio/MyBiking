using System.Text.Json.Serialization;

namespace MyBikingApi.Models.Dtos
{
    public class WheelieItemDto
    {
        public double Speed { get; set; }
        public string Distance { get; set; }
        public double MaxRotateX { get; set; }
        //public WheeliePoint WheelePoint { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        [JsonIgnore]
        public WheelieRideDto WheelieRide { get; set; }
    }
}
