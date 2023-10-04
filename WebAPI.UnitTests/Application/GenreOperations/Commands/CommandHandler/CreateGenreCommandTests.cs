using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;
using WebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.CommandHandler
{
    public class CreateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_BeShouldCreated()
        {
            //Arrange(Hazırlık)
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name="NewGenre",
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();
            
            //Assert (Doğrulama)
            var genre= _dbContext.Genres.SingleOrDefault(g=>g.Name == "NewGenre");
            genre.Should().NotBeNull();
            genre.Id.Should().BeGreaterThan(0);
            genre.Name.Should().Be("NewGenre");
            genre.IsActive.Should().BeTrue();
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_BeShouldReturn()
        {
            //Arrange(Hazırlık)
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name = "Personal Growth",
            };

            //Act & Assert (Çalıştırma  - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Mevcut");
        }

    }
}
