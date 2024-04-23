using Microsoft.EntityFrameworkCore;
using MyBiking.Application.Dtos;
using MyBiking.Application.Models;
using MyBiking.Entity.Enums;
using MyBiking.Entity.IRepository;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Infrastructure.Repository
{
    public class WheelieRideRepository : Repository<Entity.Models.WheelieRide>, IWheelieRideRepository
    {
        private readonly MyBikingDbContext _context;
        private readonly IUserHttpContext _userHttpContext;
        public WheelieRideRepository(MyBikingDbContext context, IUserHttpContext userHttpContext) : base(context)
        {
            this._userHttpContext = userHttpContext;
            _context = context;
            _userHttpContext = userHttpContext;
        }

        public async Task<Status> DeleteWheelie(int id)
        {
            var wheelieToRemove = await _context.WheelieRides.FirstOrDefaultAsync(r => r.Id == id);
            if (wheelieToRemove != null)
            {
                _context.WheelieRides.Remove(wheelieToRemove);
                _context.SaveChanges();

                return new Status()
                {
                    Message = "Ride deleted successfully",
                    StatusCode = 204
                };
            }
            return new Status()
            {
                Message = "Ride has been not deleted successfully",
                StatusCode = 400
            };
        }

        public async Task<Entity.Models.WheelieRide> GetWheelieRideById(int wheelieRideId)
        {
            return await _context.WheelieRides
                .Include(wi => wi.WheeleItems)
                .FirstOrDefaultAsync(wi => wi.Id == wheelieRideId);
        }

        public async Task<List<Entity.Models.WheelieRide>> GetWheelieRidesById(int? rideId)
        {
            var userID = _userHttpContext.GetUser()?.Id;

            var wheeleRides = await _context.WheelieRides
                .Where(w => w.RideId == rideId &&
                    w.Ride.IsPublic || w.Ride.ApplicationUserId == userID)
                .Include(w => w.Ride).ThenInclude(r => r.ApplicationUser)
                .Include(w => w.WheeleItems)
                .ToListAsync();
            if (wheeleRides.Count() > 0)
            {
                return wheeleRides;
            }
            //verify if ride is public and created by currently logged user (if logged)
            if (wheeleRides.Count() == 0 && !await _context.Rides.AnyAsync(r => r.Id == rideId && r.ApplicationUserId == userID))
            {
                return null;
            }

            return wheeleRides;
        }
    }
}
