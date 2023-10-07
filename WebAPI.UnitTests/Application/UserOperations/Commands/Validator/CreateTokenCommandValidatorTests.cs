using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.UserOperations.Commands.CommandHandler;
using WebAPI.Application.UserOperations.Commands.RequestCommandModel;
using WebAPI.Application.UserOperations.Commands.Validator;

namespace WebAPI.UnitTests.Application.UserOperations.Commands.Validator
{
    public class CreateTokenCommandValidatorTests
    {
        [Theory]
        [InlineData("","")]
        [InlineData("email", "")]
        [InlineData("", "password")]
        [InlineData("email", "pas")]
        [InlineData("ema", "password")]
        public void WhenInvalidLoginInputsAreGiven_InvalidOperationException_ShouldBeErrors(string email, string password)
        {
            //Arrange(Hazırlık)
            CreateTokenCommand command = new CreateTokenCommand(null, null, null);
            command.Model = new CreateTokenModel()
            {
                Email = email,
                Password = password
            };

            //Act (Çalıştırma)
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            var result=validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
