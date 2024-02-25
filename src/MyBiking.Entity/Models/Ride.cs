namespace MyBiking.Application.Models
{
    public class Ride
    {
        public int Id { get; set; }
        public int BikeId { get; set; }
        public List<Point> Points { get; set; }
        public List<WheelieRide> WheeleRides { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }

    }
}
