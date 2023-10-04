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
    public class CreateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("gen")]
        public void WhenInvalidGenreNamesAreGiven_InvalidOperationException_ShouldBeError(string genreName)
        {
            //Arrange(Hazırlık)
            CreateGenreCommand query = new CreateGenreCommand(null, null);
            query.Model = new CreateGenreModel()
            {
                Name= genreName
            };

            //Act (Çalıştırma)
            CreateGenreCommandValidator validator=new CreateGenreCommandValidator();
            var result= validator.Validate(query);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);    
        }
    }
}
