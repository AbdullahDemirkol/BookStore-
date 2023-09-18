using WebAPI.DataAccess;

namespace WebAPI.BookOperations.Command.CommandHandler
{
    public class DeleteBookCommand
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
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
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }
}
