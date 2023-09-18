namespace WebAPI.BookOperations.Command.RequestCommandModel
{
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
    }
}
