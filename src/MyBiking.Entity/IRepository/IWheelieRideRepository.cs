using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;

namespace MyBiking.Entity.IRepository
{
    public interface IWheelieRideRepository : IRepository<WheelieRide>
    {
        Task<Status> DeleteWheelie(int id);
        Task<WheelieRide> GetWheelieRideById(int wheelieRideId);
        Task<List<WheelieRide>> GetWheelieRidesById(int? rideId);
    }
}