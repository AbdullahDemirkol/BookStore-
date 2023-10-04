using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.BookOperations.Commands.CommandHandler;
using WebAPI.Application.BookOperations.Commands.Validator;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.Validator
{
    public class DeleteBookCommandValidatorTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WhenInvalidIdsIsGiven_Validate_ShouldBeReturnErrors(int id)
        {
            //Arrange (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.Id = id;


            //Act (Çalıştırma)
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //Arrange (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.Id = 3;

            //Act (Çalıştırma)
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}
