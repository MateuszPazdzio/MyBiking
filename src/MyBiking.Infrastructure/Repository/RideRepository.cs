using Microsoft.EntityFrameworkCore;
using MyBiking.Application.Dtos;
using MyBiking.Application.Models;
using MyBiking.Entity.Constants;
using MyBiking.Entity.IRepository;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Infrastructure.Repository
{
    public class RideRepository : Repository<Ride>, IRideRepository
    {
        private readonly MyBikingDbContext _context;
        private readonly IUserHttpContext _userHttpContext;
        public RideRepository(MyBikingDbContext context, IUserHttpContext userHttpContext) : base(context)
        {
            _context = context;
            this._userHttpContext = userHttpContext;
            _userHttpContext = userHttpContext;
        }

        public async Task<Status> CreateRide(Ride ride)
        {
            try
            {
                //ride.ApplicationUserId = "e13e6aad-17a8-4e6e-a496-52725db3be7f";
                _context.Rides.Add(ride);
                _context.SaveChanges();
                return new Status()
                {
                    StatusCode = 1,
                };
            }
            catch (Exception e)
            {
                return new Status()
                {
                    StatusCode = 0,
                    Message = e.Source
                };

            }
        }

        public async Task<Status> DeleteRide(int id)
        {
            var rideToRemove = await _context.Rides.FirstOrDefaultAsync(r => r.Id == id);
            if (rideToRemove != null)
            {
                _context.Rides.Remove(rideToRemove);
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

        public async Task<List<Ride>> GetPublicRides()
        {
            return await _context.Rides
            .AsNoTracking()
            .Where(p => p.IsPublic)
            .Include(r => r.ApplicationUser)
            .Include(r => r.WheeleRides).ThenInclude(w => w.WheeleItems)
            .Include(r => r.Points)
            .OrderByDescending(r => r.Creation_Date)
            .ToListAsync();
        }

        public Task<List<Ride>> GetRideActivitiesSelectedByYear(int? year)
        {
            var userID = _userHttpContext.GetUser()?.Id;
            if (!year.HasValue)
            {
                return _context.Rides.
                    Where(r => userID == r.ApplicationUserId).
                    OrderBy(r => r.StartingDateTime).ToListAsync();
            }

            try
            {
                var a = _context.Rides
                    .AsNoTracking()
                    .Where(r => r.StartingDateTime.Year == year)
                    .ToListAsync();
                return a;


            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public async Task<Ride> GetRideById(int id)
        {
            return await _context.Rides
            .AsNoTracking()
            .Include(r => r.WheeleRides).ThenInclude(w => w.WheeleItems)
            .Include(r => r.Points)
            .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Ride>> GetRidesByMonthAsync(string year, string month)
        {
            var userID = _userHttpContext.GetUser()?.Id;
            try
            {
                var rides = await _context.Rides
                    .AsNoTracking()
                    .Include(r => r.WheeleRides).ThenInclude(w => w.WheeleItems)
                    .Include(r => r.Points)
                    .Where(r => r.StartingDateTime.Month == Month.Months[month] &&
                        r.StartingDateTime.Year.ToString() == year &&
                        r.ApplicationUserId == userID)
                    .ToListAsync();

                return rides;
            }
            catch (Exception)
            {
                await Console.Out.WriteLineAsync();
                throw;
            }
            return null;
        }
    }
}
