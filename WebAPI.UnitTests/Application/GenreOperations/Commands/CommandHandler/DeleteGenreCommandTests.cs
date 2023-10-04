using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.CommandHandler
{
    public class DeleteGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            //Arrange(Hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = 3;

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var genre = _dbContext.Genres.SingleOrDefault(g => g.Id == command.GenreId);
            genre.Should().BeNull();
        }
        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }

    }
}
