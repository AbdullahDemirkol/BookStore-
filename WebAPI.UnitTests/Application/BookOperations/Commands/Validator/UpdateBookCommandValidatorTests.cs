using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.BookOperations.Commands.CommandHandler;
using WebAPI.Application.BookOperations.Commands.RequestCommandModel;
using WebAPI.Application.BookOperations.Commands.Validator;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.Validator
{
    public class UpdateBookCommandValidatorTests
    {
        [Theory]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 0, 0, 1)]
        [InlineData("", 0, 1, 0)]
        [InlineData("", 1, 0, 0)]
        [InlineData("", 1, 0, 1)]
        [InlineData("", 1, 1, 0)]
        [InlineData("", 1, 1, 1)]
        [InlineData("Tes", 0, 0, 0)]
        [InlineData("Tes", 0, 0, 1)]
        [InlineData("Tes", 0, 1, 0)]
        [InlineData("Tes", 1, 0, 0)]
        [InlineData("Tes", 0, 1, 1)]
        [InlineData("Tes", 1, 0, 1)]
        [InlineData("Tes", 1, 1, 0)]
        [InlineData("Tes", 1, 1, 1)]
        [InlineData("Title", 0, 0, 0)]
        [InlineData("Title", 0, 0, 1)]
        [InlineData("Title", 0, 1, 0)]
        [InlineData("Title", 1, 0, 0)]
        [InlineData("Title", 1, 0, 1)]
        [InlineData("Title", 1, 1, 0)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int authorId, int genreId,int pageCount)
        {            
            //Arrange(Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Id = 1;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                PageCount = pageCount
            };

            //Act (Çalıştırma)
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
