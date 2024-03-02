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
using MyBiking.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

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
            var result = await _userManager.CreateAsync(user, "Trucker18!");
            //var result = await _userManager.CreateAsync(user, user.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            await _userManager.AddToRoleAsync(user, "Member");

            //if (!await _roleManager.RoleExistsAsync(model.Role))
            //    await _roleManager.CreateAsync(new IdentityRole(model.Role));


            //if (await roleManager.RoleExistsAsync(model.Role))
            //{
            //    await userManager.AddToRoleAsync(user, model.Role);
            //}

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;


                //await _signInManager.SignInAsync(user, true);
                //var d = User.IsInRole("Member");
                //if (User.Identity.IsAuthenticated)
                //{
                //    await Console.Out.WriteLineAsync("");
                //}
                //return RedirectToAction("Index", "Home");
                //return Task.CompletedTask;

            //user.PasswordHashed = _passwordHasher.HashPassword(user, user.PasswordHelpers.Password);
            //user.RoleId= 3;
            //userManager.AddToRoleAsync(user, "Standard");
            //userManager.CreateAsync(user);
            //_myBikingDbContext.Users.Add(user);
            //_myBikingDbContext.SaveChanges();
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

            if (!await _userManager.CheckPasswordAsync(userFound, "Trucker18!"))
            //if (!await _userManager.CheckPasswordAsync(userFound, user.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }
            //var signInResult = await _signInManager.PasswordSignInAsync(user, userFound.Password, false, true);
            var signInResult = await _signInManager.PasswordSignInAsync(userFound, "Trucker18!", false, true);
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
