namespace WebAPI.Application.BookOperations.Commands.RequestCommandModel
{
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
    }
}
