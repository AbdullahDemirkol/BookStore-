using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Commands.CommandHandler;
using WebAPI.Application.AuthorOperations.Commands.Validator;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;
using WebAPI.Application.GenreOperations.Commands.Validator;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.Validator
{
    public class DeleteAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeErrors(int id)
        {
            //Arrange(Hazırlık)
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = id;

            //Act (Çalıştırma)
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
