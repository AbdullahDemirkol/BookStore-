using FluentAssertions;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;
using WebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.CommandHandler
{
    public class UpdateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId = 99999999;
            command.Model = new UpdateGenreModel()
            {
                Name = "newGenre",
                IsActive = true
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId = 2;
            command.Model = new UpdateGenreModel()
            {
                Name = "Noval",
                IsActive = true
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu Isimde Tür Bulunmaktadır");
        }
        [Fact]
        public void WhenValidInputIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId = 2;
            command.Model = new UpdateGenreModel()
            {
                Name = "Novall",
                IsActive = true
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var genre = _dbContext.Genres.FirstOrDefault(g => g.Id == 2);
            genre.Should().NotBeNull();
            genre.Name.Should().Be("Novall");
            genre.IsActive.Should().BeTrue();
        }
    }
}
