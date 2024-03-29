﻿using Microsoft.AspNetCore.Http;
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
            if (user == null)
            {
                throw new System.Exception("User does not exists");
            }
            CurrentUser currentUser = new CurrentUser()
            {
                UserName = user.FindFirst(c => c.Type == ClaimTypes.Name).Value,
                Id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value,
                ////Email = user.FindFirst(c => c.Type == ClaimTypes.Email).Value,
            };
            return currentUser;
        }
    }
}
