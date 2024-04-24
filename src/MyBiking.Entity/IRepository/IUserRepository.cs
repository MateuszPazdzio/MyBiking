using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Entity.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<Status> LoginUser(ApplicationUser user);
        public Task<bool> GetUserByEmail(string email);
        public Task<Status> CreateUser(ApplicationUser user);
        public Task LogoutAsync();
        public Task<Status> LoginApi(ApplicationUser user);
    }
}
