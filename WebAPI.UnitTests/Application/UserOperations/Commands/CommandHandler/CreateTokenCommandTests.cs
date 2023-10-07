using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.UserOperations.Commands.CommandHandler;
using WebAPI.Application.UserOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.UserOperations.Commands.CommandHandler
{
    public class CreateTokenCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
            _configuration = testFixture.Configuration;
        }
        [Fact]
        public void WhenValidLoginInputIsGiven_Token_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateTokenCommand command=new CreateTokenCommand(_dbContext, _mapper,_configuration);
            command.Model = new CreateTokenModel()
            {
                Email = "Test@gmail.com",
                Password = "Test"
            };

            //Act (Çalıştırma)
            var login = FluentActions.Invoking(()=>command.Handle()).Invoke();

            //Assert (Doğrulama)
            login.RefreshToken.Should().NotBeNull();
            login.AccessToken.Should().NotBeNull();
            login.ExpireDate.Should().BeOnOrAfter(DateTime.Now);
        }
        [Fact]
        public void WhenInvalidLoginInputIsGiven_Token_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper, _configuration);
            command.Model = new CreateTokenModel()
            {
                Email = "WrongTest@gmail.com",
                Password = "Test"
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı Adı yada Şifre Hatalı!");
        }
    }
}
