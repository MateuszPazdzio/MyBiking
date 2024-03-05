using System.ComponentModel.DataAnnotations;

namespace MyBiking.Application.Dtos
{
    public class RegisterUserDto
    {
        //public string FirstName { get; set; }
        //public string SecondName { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public string Nationality { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerification { get; set; }

        //public List<Nationality> Nationalities { get; set; }
    }
}