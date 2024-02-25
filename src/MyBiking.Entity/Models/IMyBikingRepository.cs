using MyBiking.Application.Models;
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
        public Task CreateUser(User user);
        string LoginUser(User user);
    }
}
