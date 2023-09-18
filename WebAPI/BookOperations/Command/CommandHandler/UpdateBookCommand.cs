using WebAPI.BookOperations.Command.RequestCommandModel;
using WebAPI.Common;
using WebAPI.DataAccess;

namespace WebAPI.BookOperations.Command.CommandHandler
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(p => p.Id == Id);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            if (!Enum.IsDefined(typeof(GenreEnum), Model.GenreId))
            {
                throw new InvalidOperationException("Bu tür bulunamadı.");
            }

            book.GenreId = Model.GenreId != default ? (GenreEnum)Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }
    }
}
