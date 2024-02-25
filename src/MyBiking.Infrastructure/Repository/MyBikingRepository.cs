using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyBiking.Application.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MyBiking.Entity.Models;

namespace MyBiking.Infrastructure.Repository
{
    public class MyBikingRepository : IMyBikingRepository
    {
        private readonly MyBikingDbContext _myBikingDbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public MyBikingRepository(MyBikingDbContext myBikingDbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._myBikingDbContext = myBikingDbContext;
            this._passwordHasher = passwordHasher;
            this._authenticationSettings = authenticationSettings;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<List<Nationality>> GetNationalities() => await _myBikingDbContext.Nationalities.ToListAsync();

        public Task CreateUser(User user)
        {

            //user.PasswordHashed = _passwordHasher.HashPassword(user, user.PasswordHelpers.Password);
            //user.RoleId= 3;
            userManager.CreateAsync(user);
            //_myBikingDbContext.Users.Add(user);
            //_myBikingDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<bool> GetUserByEmail(string email)
        {
            return await _myBikingDbContext.Users.AnyAsync(p => p.Email == email);
        }

        public string LoginUser(User user)
        {
            //User userMapped = _mapper.Map<User>(loginDto);

            //var validUser = AuthenticateUser(user);
            //var claims = GenerateListOfClaims(validUser);
            //var token = GenerateToken(claims);

            //return token;
            return "";
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
}
