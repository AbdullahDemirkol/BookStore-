using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;
using WebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using WebAPI.Application.GenreOperations.Commands.Validator;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.Validator
{
    public class UpdateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("", 999999999)]
        [InlineData("gen", 999999999)]
        [InlineData("",2)]
        [InlineData("gen",2)]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeErrors(string genreName, int genreId)
        {
            //Arrange(Hazırlık)
            UpdateGenreCommand query = new UpdateGenreCommand(null);
            query.GenreId = genreId;
            query.Model = new UpdateGenreModel()
            {
                Name = genreName,
                IsActive = true
            };

            //Act (Çalıştırma)
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(query);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
