using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Queries.QueryHandler.GetAuthor;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.AuthorOperations.Queries.QueryHandler.GetAuthor
{
    public class GetAuthorByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            GetAuthorByIdQuery query = new GetAuthorByIdQuery(_dbContext, _mapper);
            query.AuthorId = 3;

            //Act (Çalıştırma)
            var author = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert (Doğrulama)
            var registeredAuthor = _dbContext.Authors.FirstOrDefault(a => a.Id == 3);
            author.Should().NotBeNull();
            author.DateOfBirth.Should().Be(registeredAuthor.DateOfBirth);
            author.FullName.Should().Be(registeredAuthor.Name+" "+registeredAuthor.Surname);

        }
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            GetAuthorByIdQuery query = new GetAuthorByIdQuery(_dbContext, _mapper);
            query.AuthorId = 99999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı");
        }

    }
}
