using FluentValidation;
using MyBiking.Application.Models;

namespace MyBiking.Application.Validation
{
    public class LoginUserDtoRule : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoRule()
        {
            RuleFor(l => l.Email)
                .EmailAddress()
                .NotEmpty()
                .WithMessage("Email is not valid");
        }
    }
}
