using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MyBiking.Application.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public  string Id { get; set; }
        //public string FirstName { get; set; }
        //public string SecondName { get; set; }
        //public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public string? Nationality { get; set; }
        //public Nationality Nationality { get; set; }
        //public int NationalityId { get; set; }
        //public DateTime CreationDate { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }

        //public PasswordHelpers PasswordHelpers { get; set; }
        //public string PasswordHashed { get; set; }
        //public int RoleId { get; set; }
        //public virtual IdentityRole Role { get; set; }
        public List<Ride> Rides { get; set; }

    }
}
