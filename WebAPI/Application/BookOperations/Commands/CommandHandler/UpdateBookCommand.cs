using WebAPI.Application.BookOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.BookOperations.Commands.CommandHandler
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int Id { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(p => p.Id == Id);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil");
            }
            if (!_dbContext.Genres.Any(p=>p.Id==Model.GenreId && p.IsActive == true))
            {
                throw new InvalidOperationException("Böyle bir kitap türü bulunamadı");
            }
            if (!_dbContext.Authors.Any(p => p.Id == Model.AuthorId))
            {
                throw new InvalidOperationException("Böyle bir yazar bulunamadı");
            }

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }
    }
}
