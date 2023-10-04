using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Commands.CommandHandler;
using WebAPI.Application.AuthorOperations.Commands.RequestCommandModel;
using WebAPI.Application.AuthorOperations.Commands.Validator;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.Validator
{
    public class CreateAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "authorSurname")]
        [InlineData("aut", "authorSurname")]
        [InlineData("authorName", "")]
        [InlineData("authorName", "aut")]
        public void WhenInvalidAuthorNamesAndSurnamesAreGiven_InvalidOperationException_ShouldBeError(string authorName, string authorSurname)
        {
            //Arrange(Hazırlık)
            CreateAuthorCommand query = new CreateAuthorCommand(null, null);
            query.Model = new CreateAuthorModel()
            {
                Name = authorName,
                Surname = authorSurname
            };

            //Act (Çalıştırma)
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(query);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors()
        {
            //Arrange(Hazırlık)
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "authorName",
                Surname = "authorSurname",
                DateOfBirth = DateTime.Now.Date
            };

            //Act (Çalıştırma)
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
