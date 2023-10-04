using AutoMapper;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.BookOperations.Commands.CommandHandler;
using WebAPI.Application.BookOperations.Commands.Validator;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.CommandHandler
{
    public class DeleteBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeDeleted()
        {
            //Arrange(Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_dbContext);
            command.Id = 2;



            //Act (Çalıştırma)
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var book = _dbContext.Books.FirstOrDefault(p => p.Id == 2);
            book.Should().BeNull();
        }
        [Fact]
        public void WhenNonExistentIdIsGiven_Validate_InvalidOperationException_BeReturned()
        {
            //Arrange(Hazırlık)
            DeleteBookCommand command =new DeleteBookCommand(_dbContext);
            command.Id=99999;

            //Act & Assert (Çalıştırma & Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil");

        }
    }
}
