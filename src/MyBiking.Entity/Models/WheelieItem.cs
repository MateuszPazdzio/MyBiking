namespace MyBiking.Entity.Models
{
    public class WheelieItem
    {
        public int Id { get; set; }
        public double Speed { get; set; }
        public string Address { get; set; }
        public string Distance { get; set; }
        public double MaxRotateX { get; set; }
        public WheeliePoint WheelePoint { get; set; }// ->new WheelePoint(){} mapowana na podstawie PointDto. WheelePointDto nie bedzie istniec
        public double Altitude { get; set; }
        public  WheelieRide WheelieRide { get; set; }
        public int WheelieRideId { get; set; }

    }
}