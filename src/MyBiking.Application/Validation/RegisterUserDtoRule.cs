using FluentValidation;
using MyBiking.Application.Dtos;
using MyBiking.Entity.Models;
using System.Linq;

namespace MyBiking.Application.Validation
{
    public class RegisterUserDtoRule : AbstractValidator<RegisterUserDto>
    {
        private readonly IMyBikingRepository _context;

        public RegisterUserDtoRule(IMyBikingRepository context)
        {
            this._context = context;

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("Email is not valid")
                .Custom((value, context) =>
                {
                    if (_context.GetUserByEmail(value).Result)
                    {
                        context.AddFailure("User with this email already exists");
                    }
                }).NotEmpty();

            RuleFor(u => u.Password)
                .Length(8,18)
                .WithMessage("Password must be between 8-18 characters")
                .Matches(@".*\d.*\d.*[^\w]")
                .WithMessage("At least two characters must be integers and at least one character must be non-alphanumeric");

            RuleFor(u => u.Password)
                .Equal(u => u.PasswordVerification)
                .WithMessage("Passwords do not match ");

            RuleFor(u => u.UserName)
                .Matches(@"[a-zA-Z\d']{3,20}")
                .WithMessage("UserName must be less than 20 characters and greater than 3 characters. Use only letters or digits.")
                .NotEmpty();

            //RuleFor(u => u.SecondName)
            //    .Matches(@"[a-zA-Z' -]{3,25}")
            //    .WithMessage("SecondName must be less than 20 characters")
            //    .NotEmpty();

            //RuleFor(u => u.Nationality)
            //    .Must(value => !string.IsNullOrEmpty(value) && value!="Select...")
            //    .WithMessage(value => $"{value.Nationality} nationality does not exists");

            RuleFor(u => u.DateOfBirth)
                .Must(val=>(DateTime.Today.AddYears(-18)>=val))
                .WithMessage("You must have more than 18 years old.")
                .NotEmpty();

        }
    }
}
