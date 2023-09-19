using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Entity.Enum;

namespace WebAPI.Entity.Concrete
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public GenreEnum GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
