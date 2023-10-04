using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Queries.QueryViewModel;
using WebAPI.Application.BookOperations.Queries.QueryHandler.GetBook;
using WebAPI.Application.BookOperations.Queries.QueryViewModel;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.BookOperations.Queries.QueryHandler.GetBook
{
    public class GetBookByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeError()
        {            
            //Assert (Doğrulama)
            GetBookByIdQuery query = new GetBookByIdQuery(_dbContext, _mapper);
            query.Id = 0;

            //Act & Arrange (Çalıştırma - Hazırlık)
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Assert (Doğrulama)
            GetBookByIdQuery query = new GetBookByIdQuery(_dbContext, _mapper);
            query.Id = 1;

            //Act (Çalıştırma)
            var book = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Arrange (Hazırlık)
            var registeredBook = _dbContext.Books.Include(p=>p.Author).Include(p=>p.Genre).FirstOrDefault(b => b.Id == 1);
            book.Should().NotBeNull();
            book.Title.Should().Be(registeredBook.Title);
            book.Author.Should().Be(registeredBook.Author.Name+" "+registeredBook.Author.Surname);
            book.Genre.Should().Be(registeredBook.Genre.Name);
            book.PageCount.Should().Be(registeredBook.PageCount);
            book.PublishDate.Should().Be(registeredBook.PublishDate.ToString());
        }
    }
}
