using System.ComponentModel.DataAnnotations;

namespace MyBiking.Entity.Models
{
    public class Ride
    {
        public int Id { get; set; }
        //public int BikeId { get; set; }
        public List<Point> Points { get; set; }
        public List<WheelieRide> WheeleRides { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public double Distance { get; set; }
        

    }
}
