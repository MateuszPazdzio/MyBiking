namespace MyBiking.Entity.Models
{
    public class WheelieRide
    {
        public int Id { get; set; }
        public List<WheelieItem> WheeleItems { get; set; } 
        public string DurationTime { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public double Distance { get; set; }
        public  Ride Ride { get; set; }
        public int RideId { get; set; }
    }
}
