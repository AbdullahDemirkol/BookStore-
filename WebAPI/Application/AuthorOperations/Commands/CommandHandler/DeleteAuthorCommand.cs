using AutoMapper;
using Microsoft.OpenApi.Any;
using WebAPI.DataAccess;

namespace WebAPI.Application.AuthorOperations.Commands.CommandHandler
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.FirstOrDefault(a => a.Id == AuthorId);
            if(author is null)
            {
                throw new InvalidOperationException("Yazar mevcut değil");
            }
            bool AnyHasBooks = _dbContext.Books.Any(p => p.AuthorId == AuthorId);
            if (AnyHasBooks)
            {
                throw new InvalidOperationException("Kitabı yayında olan bir yazar silinemez. Öncelikle yazarın yayında olan kitapları silinmeli");
            }
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
