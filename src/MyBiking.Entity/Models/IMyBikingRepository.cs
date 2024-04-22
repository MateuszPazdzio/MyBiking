using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Entity.Models
{
    public interface IMyBikingRepository
    {
        public Task<List<Nationality>> GetNationalities();
        public Task<bool> GetUserByEmail(string email);
        public Task<Status> CreateUser(ApplicationUser user);
        public Task<Status> LoginUser(ApplicationUser user);
        public Task LogoutAsync();
        public Task<Status> CreateRide(Ride ride);
        public Task<List<Ride>> GetRidesByMonthAsync(string year,string month);
        public Task<List<Ride>> GetRideActivitiesSelectedByYear(int? year);
        public Task<List<WheelieRide>> GetWheelieRidesById(int? rideId);
        public Task<Ride> GetRideById(int id);
        public Task<WheelieRide> GetWheelieRideById(int wheelieRideId);
        public Task<Status> LoginApi(ApplicationUser user);
        public Task<Status> DeleteWheelie(int id);
        public Task<Status> DeleteRide(int id);
        public Task<List<Ride>> GetPublicRides();
    }
}
