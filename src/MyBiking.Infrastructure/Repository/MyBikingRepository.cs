using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyBiking.Entity.Models;
using MyBiking.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.Internal;
using System.Globalization;
using MyBiking.Entity.Constants;

namespace MyBiking.Infrastructure.Repository
{
    public class MyBikingRepository : IMyBikingRepository
    {
        private readonly MyBikingDbContext _myBikingDbContext;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MyBikingRepository(MyBikingDbContext myBikingDbContext, IPasswordHasher<ApplicationUser> passwordHasher,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._myBikingDbContext = myBikingDbContext;
            this._passwordHasher = passwordHasher;
            //this._authenticationSettings = authenticationSettings;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<List<Nationality>> GetNationalities() => await _myBikingDbContext.Nationalities.ToListAsync();

        public async Task<Status> CreateUser(ApplicationUser user)
        {
            var status = new Status();
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exist";
                return status;
            }
            var result = await _userManager.CreateAsync(user, user.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            await _userManager.AddToRoleAsync(user, "Member");


            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;

        }

        public async Task<bool> GetUserByEmail(string email)
        {
            return await _myBikingDbContext.Users.AnyAsync(p => p.Email == email);
        }

        public async Task<Status> LoginUser(ApplicationUser user)
        {
            var status = new Status();
            var userFound = await _userManager.FindByNameAsync(user.UserName);

            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(userFound, user.Password))
            //if (!await _userManager.CheckPasswordAsync(userFound, user.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }
            //var signInResult = await _signInManager.PasswordSignInAsync(user, userFound.Password, false, true);
            var signInResult = await _signInManager.PasswordSignInAsync(userFound, user.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }

            return status;

            //var validUser = AuthenticateUser(user);
            //var claims = GenerateListOfClaims(validUser);
            //var token = GenerateToken(claims);

            //return token;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Status> CreateRide(Ride ride)
        {
            try
            {
                //ride.ApplicationUserId = "e13e6aad-17a8-4e6e-a496-52725db3be7f";
                 _myBikingDbContext.Rides.Add(ride);
                 _myBikingDbContext.SaveChanges();
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
                };

            }
        }

        public async Task<List<Ride>> GetRidesByMonthAsync(string year,string month)
        {
            try
            {
                var rides = await _myBikingDbContext.Rides
                    .AsNoTracking()
                    .Include(r => r.WheeleRides).ThenInclude(w=>w.WheeleItems)
                    .Include(r => r.Points)
                    .Where(r => r.StartingDateTime.Month == Month.Months[month] && 
                        r.StartingDateTime.Year.ToString()==year)
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


        public Task<List<Ride>> GetRideActivitiesSelectedByYear(int? year)
        {
            if(!year.HasValue)
            {
                return _myBikingDbContext.Rides.OrderBy(r=>r.StartingDateTime).ToListAsync();
            }

            try
            {
                var a = _myBikingDbContext.Rides
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
            return await _myBikingDbContext.Rides
                .AsNoTracking()
                .Include(r => r.WheeleRides).ThenInclude(w => w.WheeleItems)
                .Include(r => r.Points)
                .FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<List<WheelieRide>> GetWheelieRidesById(int? rideId)
        {
            var wheeleRides =await _myBikingDbContext.WheelieRides
                .Where(w=>w.Id == rideId)
                .Include(w=>w.WheeleItems)
                .ToListAsync();

            return wheeleRides;
        }

        public async Task<WheelieRide> GetWheelieRideById(int wheelieRideId)
        {
            return await _myBikingDbContext.WheelieRides
                .Include(wi=>wi.WheeleItems)
                .FirstOrDefaultAsync(wi => wi.Id == wheelieRideId);
        }

        //private User AuthenticateUser(User userMapped)
        //{
        //    var validUser = _myBikingDbContext.Users.Include(user => user.Role).
        //        FirstOrDefault(user => user.Email == userMapped.Email);

        //    if (validUser == null)
        //    {
        //        //throw new WrongCredentialsException("Email does not exists");
        //    }

        //    var result = _passwordHasher.VerifyHashedPassword(validUser, validUser.PasswordHashed, userMapped.PasswordHelpers.Password);
        //    if (PasswordVerificationResult.Failed == result)
        //    {
        //        //throw new WrongCredentialsException("Wrong password");
        //    }

        //    return validUser;
        //}

        //private List<Claim> GenerateListOfClaims(User validUser)
        //{
        //    return new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.NameIdentifier,validUser.Id.ToString()),
        //        new Claim(ClaimTypes.Email,validUser.Email),
        //        new Claim(ClaimTypes.Name, validUser.FirstName),
        //        new Claim(ClaimTypes.Role,validUser.Role.Name),
        //    };
        //}
        //private string GenerateToken(List<Claim> claims)
        //{
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        //    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        //    var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
        //        claims: claims, expires: expires, signingCredentials: cred);
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    return tokenHandler.WriteToken(token);
        //}
    }

    //internal class RideTimeActivityYearComparer : IComparer<RideTimeActivity>
    //{

    //    public int Compare(RideTimeActivity? x, RideTimeActivity? y)
    //    {
    //        return x.Year.CompareTo(y.Year);
    //    }
    //}

    //internal class RideTimeActivityDatesComparer : IComparer<DateTime>
    //{
    //    public int Compare(DateTime x, DateTime y)
    //    {
    //        return x.CompareTo(y);
    //    }
    //}
}
