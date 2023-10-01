using AutoMapper;
using WebAPI.Application.AuthorOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.AuthorOperations.Commands.CommandHandler
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var author=_dbContext.Authors.FirstOrDefault(a=>a.Id==AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar mevcut değil");
            }
            if (_dbContext.Authors.Any(a => a.Name.ToLower().Replace(" ","") == Model.Name.ToLower().Replace(" ","") 
            && a.Surname.ToLower().Replace(" ", "") == Model.Surname.ToLower().Replace(" ", "") 
            && DateTime.Equals(a.DateOfBirth, Model.DateOfBirth)
            && a.Id != AuthorId))
            {
                throw new InvalidOperationException("İsim ve soyisime ait yazar bulunmaktadır");
            }
            author.Name = string.IsNullOrEmpty(Model.Name) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname) ? author.Surname : Model.Surname;
            author.DateOfBirth = Model.DateOfBirth;
            _dbContext.SaveChanges();
        }

    }
}
