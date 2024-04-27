using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyBiking.Application.Dtos;
using MyBiking.Entity.IRepository;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Infrastructure.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly MyBikingDbContext _context;

        public UserRepository(MyBikingDbContext context, IPasswordHasher<ApplicationUser> passwordHasher,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AuthenticationSettings authenticationSettings,
            IUserHttpContext userHttpContext) : base(context)
        {
            _context = context;
            this._passwordHasher = passwordHasher;
            this._authenticationSettings = authenticationSettings;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<Status> CreateUser(ApplicationUser user)
        {
            var status = new Status();
            var userExists = await _userManager.FindByNameAsync(user.UserName);
            if (userExists != null)
            {
                status.Code = Entity.Enums.Code.HTTP400;
                status.Message = "User already exist";
                return status;
            }
            var result = await _userManager.CreateAsync(user, user.Password);
            if (!result.Succeeded)
            {
                status.Code= Entity.Enums.Code.HTTP400;
                status.Message = "User creation failed";
                return status;
            }

            await _userManager.AddToRoleAsync(user, "Member");

            status.Code = Entity.Enums.Code.HTTP201;
            status.Message = "You have registered successfully";
            return status;
        }

        public async Task<bool> GetUserByEmail(string email)
        {
            return await _context.Users.AnyAsync(p => p.Email == email);
        }

        public async Task<bool> GetUserByUserName(string usernname)
        {
            return await _context.Users.AnyAsync(p => p.UserName == usernname);
        }

        public async Task<Status> LoginApi(ApplicationUser user)
        {
            var validUser = await AuthenticateUser(user);
            var claims = await GenerateListOfClaims(validUser);
            var token = await GenerateToken(claims);

            return new Status()
            {
                StatusCode = 200,
                Message = token.ToString()
            };
        }

        public async Task<Status> LoginUser(ApplicationUser user)
        {
            var status = new Status();
            var userFound = await _userManager.FindByNameAsync(user.UserName);

            if (user == null)
            {
                status.Code = Entity.Enums.Code.HTTP400;
                status.Message = "Invalid username";
                return status;
            }

            if (!await _userManager.CheckPasswordAsync(userFound, user.Password))
            //if (!await _userManager.CheckPasswordAsync(userFound, user.Password))
            {
                status.Code = Entity.Enums.Code.HTTP500;
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
                status.Code = Entity.Enums.Code.HTTP500;
                status.Message = "User is locked out";
            }
            else
            {
                status.Code = Entity.Enums.Code.HTTP400;
                status.Message = "Error on logging in";
            }

            return status;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<ApplicationUser> AuthenticateUser(ApplicationUser userMapped)
        {
            var validUser = await _context.Users
                .FirstOrDefaultAsync(user => user.UserName == userMapped.UserName);
            if (validUser == null)
            {
                //throw new WrongCredentialsException("Email does not exists");
            }
            var result = _passwordHasher.VerifyHashedPassword(validUser, validUser.PasswordHash, userMapped.Password);
            if (PasswordVerificationResult.Failed == result)
            {
                //throw new WrongCredentialsException("Wrong password");
            }
            return validUser;
        }
        private async Task<List<Claim>> GenerateListOfClaims(ApplicationUser validUser)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,validUser.Id.ToString()),
                new Claim(ClaimTypes.Email,validUser.Email),
                new Claim(ClaimTypes.Name, validUser.UserName),
                new Claim(ClaimTypes.Role, String.Join(',',await _userManager.GetRolesAsync(validUser))),
            };
        }
        private async Task<string> GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims: claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
