namespace MyBiking.Application.ViewModels
{
    internal class MonthlyAgregatedRideResponse
    {
        public double Distance { get; set; }
        public int Rides { get; set; }
        public double? WheelieMaxV { get; set; }
        public double TotalWheelieDistance { get; set; }
        public int? Wheelies { get; set; }

    }
}