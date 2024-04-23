using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Entity.IRepository
{
    public interface IRideRepository : IRepository<Ride>
    {
        public Task<Status> CreateRide(Ride ride);
        public Task<List<Ride>> GetRidesByMonthAsync(string year, string month);
        public Task<List<Ride>> GetRideActivitiesSelectedByYear(int? year);
        public Task<Ride> GetRideById(int id);
        public Task<Status> DeleteRide(int id);
        public Task<List<Ride>> GetPublicRides();
    }
}
