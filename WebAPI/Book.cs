namespace WebAPI
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
