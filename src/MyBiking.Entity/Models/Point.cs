namespace MyBiking.Entity.Models
{
    public class Point
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public Ride Ride { get; set; }
        public int RideId { get; set; }
    }
}
