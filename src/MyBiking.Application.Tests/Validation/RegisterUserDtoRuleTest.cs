using FluentValidation.TestHelper;
using Moq;
using MyBiking.Application.Dtos;
using MyBiking.Application.Functions.Command.User;
using MyBiking.Application.Validation;
using MyBiking.Entity.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Tests.Validation
{
    [TestFixture]
    public class RegisterUserDtoRuleTest
    {
        private RegisterUserDtoRule _validator;
        private Mock<IUserRepository> _userRepositoryMock;
        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _validator = new RegisterUserDtoRule(_userRepositoryMock.Object);
        }

        [Test]
        public void Validate_Email_InvalidFormat_FailsValidation()
        {
            // arrange
            var registerUserDto = new RegisterUserDtoCommand { Email = "invalidemail" };

            // act
            var result = _validator.TestValidate(registerUserDto);

            // assert
            result.ShouldHaveValidationErrorFor(u => u.Email)
                  .WithErrorMessage("Email is not valid");
        }

        [Test]
        public void Validate_Email_AlreadyExists_FailsValidation()
        {
            // arrange
            var existingEmail = "existing@example.com";
            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(existingEmail)).ReturnsAsync(true);
            var registerUserDto = new RegisterUserDtoCommand { Email = existingEmail };

            // act
            var result = _validator.TestValidate(registerUserDto);

            // assert
            result.ShouldHaveValidationErrorFor(u => u.Email)
                  .WithErrorMessage("User with this email already exists");
        }


        [Test]
        public void Validate_Passwords_DoNotMatch_FailsValidation()
        {
            // arrange
            var registerUserDto = new RegisterUserDtoCommand { Password = "password1", PasswordVerification = "password2" };

            // act
            var result = _validator.TestValidate(registerUserDto);

            // assert
            result.ShouldHaveValidationErrorFor(u => u.Password)
                  .WithErrorMessage("Passwords do not match");
        }
    }
}
