using AutoMapper;
using WebAPI.Application.TokenOperations.Commands.CommandHandler;
using WebAPI.Application.TokenOperations.Commands.Models;
using WebAPI.Application.UserOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.UserOperations.Commands.CommandHandler
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var user= _dbContext.Users.FirstOrDefault(u=>u.Email==Model.Email&& u.Password==Model.Password);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);
                user.RefreshToken=token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı Adı yada Şifre Hatalı!");
            }

        }

    }
}
