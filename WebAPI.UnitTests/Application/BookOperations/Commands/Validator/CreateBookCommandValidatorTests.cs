using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Commands.CommandHandler;
using WebAPI.Application.BookOperations.Commands.CommandHandler;
using WebAPI.Application.BookOperations.Commands.RequestCommandModel;
using WebAPI.Application.BookOperations.Commands.Validator;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.Validator
{
    public class CreateBookCommandValidatorTests
    {
        // Theory, Fact gibi bir test methodunun sadece parametresini değiştirerek istediğim kadar çalıştırmamıza yarıyor.
        // Yani defalarca Fact kullanmak yerine sadece Theory ile parametre değiştirmemiz yeterli.
        // Datetime bir istisna onun için Fact kullanmamız gerekiyor.
        [Theory]
        [InlineData("Testt", 0, 0, 0)]
        [InlineData("Testt", 0, 0, 1)]
        [InlineData("Testt", 0, 1, 0)]
        [InlineData("Testt", 1, 0, 0)]
        [InlineData("Testt", 0, 1, 1)]
        [InlineData("Testt", 1, 0, 1)]
        [InlineData("Testt", 1, 1, 0)]
        [InlineData("Tes", 0, 0, 0)]
        [InlineData("Tes", 0, 0, 1)]
        [InlineData("Tes", 0, 1, 0)]
        [InlineData("Tes", 1, 0, 0)]
        [InlineData("Tes", 0, 1, 1)]
        [InlineData("Tes", 1, 0, 1)]
        [InlineData("Tes", 1, 1, 0)]
        [InlineData("Tes", 1, 1, 1)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 0, 0, 1)]
        [InlineData("", 0, 1, 0)]
        [InlineData("", 1, 0, 0)]
        [InlineData("", 0, 1, 1)]
        [InlineData("", 1, 0, 1)]
        [InlineData("", 1, 1, 0)]
        [InlineData("", 1, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId, int authorId, int pageCount)
        {
            //Arrange(Hazırlık)
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            //Act (Çalıştırma)
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result=validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors()
        {
            //Arrange(Hazırlık)
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Test",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 1,
                PublishDate = DateTime.Now.Date
            };

            //Act (Çalıştırma)
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //Arrange(Hazırlık)
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Test",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 1,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            //Act (Çalıştırma)
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }

    }
}
