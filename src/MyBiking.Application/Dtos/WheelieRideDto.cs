using MyBiking.Application.Dtos;
using System.Text.Json.Serialization;

namespace MyBikingApi.Models.Dtos
{
    public class WheelieRideDto
    {
        public List<WheelieItemDto> WheeleItems { get; set; }
        public string DurationTime { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public string Distnace { get; set; }
        [JsonIgnore]
        public RideDto Ride { get; set; }
    }
}
