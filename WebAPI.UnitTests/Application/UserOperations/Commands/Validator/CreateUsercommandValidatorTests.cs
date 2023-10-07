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
    public class CreateUsercommandValidatorTests
    {
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("Name", "", "", "")]
        [InlineData("", "Surname", "", "")]
        [InlineData("", "", "Email", "")]
        [InlineData("", "", "", "Password")]
        [InlineData("Name", "Surname", "", "")]
        [InlineData("Name", "", "Email", "")]
        [InlineData("Name", "", "", "Password")]
        [InlineData("", "Surname", "Email", "")]
        [InlineData("", "Surname", "", "Password")]
        [InlineData("", "", "Email", "Password")]
        [InlineData("Name", "Surname", "Email", "")]
        [InlineData("Name", "Surname", "", "Password")]
        [InlineData("Name", "", "Email", "Password")]
        [InlineData("", "Surname", "Email", "Password")]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeErrors(string name,string surname, string email, string password)
        {
            //Arrange(Hazırlık)
            CreateUserCommand command = new CreateUserCommand(null, null);
            command.Model = new CreateUserModel()
            {
                Name=name,
                Surname=surname,
                Email=email,
                Password=password
            };

            //Act (Çalıştırma)
            CreateUserCommandValidator validator=new CreateUserCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(1);

        }
    }
}
