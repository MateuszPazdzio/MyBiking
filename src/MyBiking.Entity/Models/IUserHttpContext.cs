using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Entity.Models
{
    public interface IUserHttpContext
    {
        public CurrentUser GetUser();
    }
}
