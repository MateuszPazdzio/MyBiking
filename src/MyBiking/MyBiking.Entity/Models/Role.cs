using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyBiking.Application.Models
{
    public class Role : IdentityRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}