using System.ComponentModel.DataAnnotations;

namespace MyBiking.Entity.Models
{
    public class PasswordHelpers
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string PasswordVerification { get; set; }
    }
}