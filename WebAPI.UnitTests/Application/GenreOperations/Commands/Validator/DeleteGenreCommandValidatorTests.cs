using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;
using WebAPI.Application.GenreOperations.Commands.Validator;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.Validator
{
    public class DeleteGenreCommandValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeErrors(int id)
        {
            //Arrange(Hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = id;

            //Act (Çalıştırma)
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
