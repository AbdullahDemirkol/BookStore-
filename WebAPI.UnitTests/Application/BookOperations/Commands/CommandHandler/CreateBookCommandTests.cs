using AutoMapper;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.BookOperations.Commands.CommandHandler;
using WebAPI.Application.BookOperations.Commands.RequestCommandModel;
using WebAPI.Application.BookOperations.Commands.Validator;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.CommandHandler
{
    public class CreateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateBookCommand command = new CreateBookCommand(_dbContext, _mapper);
            var newModel = new CreateBookModel()
            {
                //Id = 1,
                Title = "Testt",
                GenreId = 1,
                AuthorId = _dbContext.Authors.FirstOrDefault(p => p.Name == "Eric" && p.Surname == "Ries").Id,
                PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10)
            };
            command.Model = newModel;

            CreateBookCommandValidator validator= new CreateBookCommandValidator();

            //Act (Çalıştırma)
            FluentActions.Invoking(() => validator.ValidateAndThrow(command)).Invoke();
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var book = _dbContext.Books.SingleOrDefault(b => b.Title == "Testt");
            book.Equals(newModel);
            book.GenreId.Should().Be(newModel.GenreId);
            book.AuthorId.Should().Be(newModel.AuthorId);
            book.PageCount.Should().Be(newModel.PageCount);
            book.PublishDate.Should().Be(newModel.PublishDate);

        }


        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange(Hazırlık)
            var book = new Book()
            {
                //Id = 1,
                Title = "Test",
                GenreId = 1,
                AuthorId = _dbContext.Authors.FirstOrDefault(p => p.Name == "Eric" && p.Surname == "Ries").Id,
                PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10)
            };
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_dbContext, _mapper);
            command.Model = new CreateBookModel()
            {
                Title = book.Title,
                GenreId = 1,
                AuthorId = _dbContext.Authors.FirstOrDefault(p => p.Name == "Eric" && p.Surname == "Ries").Id,
                PageCount = 200,
                PublishDate = new DateTime(1990, 01, 10)
            };


            //Act & Assert(Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");

        }
    }
}
