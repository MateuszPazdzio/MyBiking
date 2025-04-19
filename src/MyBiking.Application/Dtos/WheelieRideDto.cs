using MyBiking.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyBikingApi.Models.Dtos
{
    public class WheelieRideDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public List<WheelieItemDto> WheeleItems { get; set; }
        [DisplayFormat(DataFormatString = "{0} s")]
        public string DurationTime { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        [Display(Name = "Distance")]
        [DisplayFormat(DataFormatString ="{0} m")]
        public double Distance { get; set; }
        [JsonIgnore]
        public RideDto Ride { get; set; }
    }
}
