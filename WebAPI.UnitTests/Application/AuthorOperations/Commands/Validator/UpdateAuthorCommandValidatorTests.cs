using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Commands.CommandHandler;
using WebAPI.Application.AuthorOperations.Commands.RequestCommandModel;
using WebAPI.Application.AuthorOperations.Commands.Validator;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.Validator
{
    public class UpdateAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "authorSurname")]
        [InlineData("aut", "authorSurname")]
        [InlineData("authorName", "")]
        [InlineData("authorName", "aut")]
        public void WhenInvalidAuthorNamesAndSurnamesAreGiven_InvalidOperationException_ShouldBeError(string authorName, string authorSurname)
        {
            //Arrange(Hazırlık)
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                Name = authorName,
                Surname = authorSurname
            };

            //Act (Çalıştırma)
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors()
        {
            //Arrange(Hazırlık)
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                Name = "authorName",
                Surname = "authorSurname",
                DateOfBirth = DateTime.Now.Date
            };

            //Act (Çalıştırma)
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
