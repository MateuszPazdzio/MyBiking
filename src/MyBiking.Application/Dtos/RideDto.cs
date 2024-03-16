using MyBikingApi.Models.Dtos;

namespace MyBiking.Application.Dtos
{
    public class RideDto
    {
        public int BikeId { get; set; }
        public List<PointDto> Points { get; set; }
        //public int UserId { get; set; }
        public List<WheelieRideDto> WheeleRides { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public double Distance { get; set; }
    }
}
