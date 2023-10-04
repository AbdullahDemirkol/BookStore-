using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.BookOperations.Commands.CommandHandler;
using WebAPI.Application.BookOperations.Commands.RequestCommandModel;
using WebAPI.Application.BookOperations.Commands.Validator;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.CommandHandler
{
    public class UpdateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //Arrange(Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(_dbContext);
            command.Id = 3;
            command.Model = new UpdateBookModel()
            {
                Title = "Testtt",
                AuthorId = 3,
                GenreId = 2,
                PageCount = 150
            };


            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var registeredBook = _dbContext.Books.FirstOrDefault(b => b.Id == 3);
            registeredBook.Should().NotBeNull();
            registeredBook.Title.Should().Be(command.Model.Title);
            registeredBook.AuthorId.Should().Be(command.Model.AuthorId);
            registeredBook.GenreId.Should().Be(command.Model.GenreId);
            registeredBook.PageCount.Should().Be(command.Model.PageCount);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        public void WhenAlreadyExistBookPropertiesAreGiven_InvalidOperationException_ShouldBeReturn(int id, int authorId, int genreId)
        {
            //Arrange(Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(_dbContext);
            command.Id=id;
            command.Model = new UpdateBookModel()
            { 
                Title = "UpdateTesttt",
                AuthorId = authorId,
                GenreId = genreId,
                PageCount = 150
            };
            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }
    }
}
