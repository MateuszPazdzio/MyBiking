using System.ComponentModel.DataAnnotations;

namespace MyBiking.Application.Models
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerified { get; set; }
    }
}