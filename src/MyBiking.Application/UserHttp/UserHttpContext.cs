using Microsoft.AspNetCore.Http;
using MyBiking.Entity.Models;
using System.Security.Claims;

namespace MyBiking.Application.Models
{
    public class UserHttpContext : IUserHttpContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser GetUser()
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }
            CurrentUser currentUser;

            try
            {
                currentUser = new CurrentUser()
                {
                    UserName = user.FindFirst(c => c.Type == ClaimTypes.Name).Value,
                    Id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value,
                    ////Email = user.FindFirst(c => c.Type == ClaimTypes.Email).Value,
                };
            }
            catch (Exception e)
            {
                return null;
            }

            return currentUser;
        }
    }
}
