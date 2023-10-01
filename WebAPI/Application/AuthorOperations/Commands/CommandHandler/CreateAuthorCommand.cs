using AutoMapper;
using WebAPI.Application.AuthorOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;

namespace WebAPI.Application.AuthorOperations.Commands.CommandHandler
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author=_dbContext.Authors.FirstOrDefault(a=>a.Name.ToLower().Replace(" ", "") == Model.Name.ToLower().Replace(" ", "") 
            && a.Surname.ToLower().Replace(" ", "") == Model.Surname.ToLower().Replace(" ", "")
            && DateTime.Equals(a.DateOfBirth,Model.DateOfBirth));
            if (author is not null)
            {
                throw new InvalidOperationException("Yazar zaten mevcut");
            }
            author =_mapper.Map<Author>(Model);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
    }
}
