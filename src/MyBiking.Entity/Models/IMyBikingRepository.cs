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
        Task LogoutAsync();
        public Task<Status> CreateRide(Ride ride);
        Task<List<Ride>> GetRidesByMonthAsync(string year,string month);
        Task<List<Ride>> GetRideActivitiesSelectedByYear(int? year);
        Task<List<WheelieRide>> GetWheelieRidesById(int? rideId);
        Task<Ride> GetRideById(int id);
        Task<WheelieRide> GetWheelieRideById(int wheelieRideId);
        Task<Status> LoginApi(ApplicationUser user);
    }
}
