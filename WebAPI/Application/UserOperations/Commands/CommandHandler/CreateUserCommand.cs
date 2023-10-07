using AutoMapper;
using WebAPI.Application.UserOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;

namespace WebAPI.Application.UserOperations.Commands.CommandHandler
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var user=_dbContext.Users.FirstOrDefault(u=>u.Email==Model.Email);
            if (user is not null)
            {
                throw new InvalidOperationException("Kullanıcı zaten mevcut");
            }
            user = _mapper.Map<User>(Model);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
