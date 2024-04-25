using Microsoft.EntityFrameworkCore;
using MyBiking.Application.Dtos;
using MyBiking.Application.Models;
using MyBiking.Entity.Constants;
using MyBiking.Entity.Enums;
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
        }

        public async Task<Status> CreateRide(Ride ride)
        {
            Status status = new Status();
            if (ride == null)
            {
                status.Code = Code.HTTP400;
                return status;
            }

            _context.Rides.Add(ride);
            _context.SaveChanges();
            status.Code = Code.HTTP201;
            return status;
        }

        public async Task<Status> DeleteRide(int id)
        {
            Status status = new Status();
            //if (id == null)
            //{
            //    status.Code = Code.HTTP400;
            //    return status;
            //}

            var rideToRemove = await _context.Rides.FirstOrDefaultAsync(r => r.Id == id);
            if (rideToRemove != null)
            {
                _context.Rides.Remove(rideToRemove);
                _context.SaveChanges();

                status.Message = "Ride deleted successfully";
                status.Code = Code.HTTP204;
                return status;
            }

            status.Message = "Ride has been not deleted successfully";
            status.Code = Code.HTTP500;
            return status;

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
            var userId = _userHttpContext.GetUser()?.Id;

            try
            {
                if (userId == null)
                {
                    throw new Exception("User is not logged in");
                }

                if (!year.HasValue)
                {
                    return _context.Rides.
                        Where(r => userId == r.ApplicationUserId).
                        OrderByDescending(r => r.Creation_Date).ToListAsync();
                }

                var rides = _context.Rides
                    .AsNoTracking()
                    .Where(r => r.StartingDateTime.Year == year)
                    .ToListAsync();

                return rides;


            }
            catch (Exception)
            {
                return null;
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
            var userId = _userHttpContext.GetUser()?.Id;
            try
            {
                if (userId == null ||
                    year == null || 
                    month==null || 
                    !Month.Months.ContainsKey(month))
                {
                    throw new Exception("Error occured, while validating request to database");
                }

                var rides = await _context.Rides
                    .AsNoTracking()
                    .Include(r => r.WheeleRides).ThenInclude(w => w.WheeleItems)
                    .Include(r => r.Points)
                    .Where(r => r.StartingDateTime.Month == Month.Months[month] &&
                        r.StartingDateTime.Year.ToString() == year &&
                        r.ApplicationUserId == userId)
                    .ToListAsync();

                return rides;
            }
            catch (Exception)
            {
                return null;

            }
        }
    }
}
