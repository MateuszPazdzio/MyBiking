using MyBiking.Entity.Models;
using MyBikingApi.Models.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBiking.Application.Dtos
{
    public class RideDto
    {
        [JsonIgnore]
        public int? Id { get; set; }
        //public int BikeId { get; set; }
        public List<PointDto>? Points { get; set; }
        //public string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public List<WheelieRideDto>? WheeleRides { get; set; }
        [Display(Name = "Start")]

        public DateTime StartingDateTime { get; set; }
        [Display(Name = "End")]

        public DateTime EndingDateTime { get; set; }
        [Display(Name = "Distance (m)")]
        public double Distance { get; set; }
        [Display(Name ="Public")]
        public bool IsPublic { get; set; }
        public DateTime Creation_Date{ get; set; }
    }
}
