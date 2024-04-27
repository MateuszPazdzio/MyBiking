using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBiking.Entity.Models;

namespace MyBiking.Entity.IRepository
{
    public interface IUserHttpContext
    {
        public CurrentUser GetUser();
    }
}
