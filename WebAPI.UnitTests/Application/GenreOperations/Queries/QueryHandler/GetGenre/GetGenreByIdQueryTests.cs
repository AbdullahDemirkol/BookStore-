using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.GenreOperations.Queries.QueryHandler.GetGenre
{
    public class GetGenreByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidIdIsGiven_Genre_ShouldBeReturn()
        {
            //Arrange(Hazırlık)
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
            query.GenreId = 4;
            var p = _dbContext.Genres.ToList();


            //Act (Çalıştırma)
            var genre = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert (Doğrulama)
            var registeredGenre =_dbContext.Genres.FirstOrDefault(g=>g.Id == 4);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(registeredGenre.Name);
        }
        [Fact]
        public void WhenInValidIdIsGiven_Genre_ShouldBeError()
        {
            //Arrange(Hazırlık)
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
            query.GenreId = 99999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }
    }
}
