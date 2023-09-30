using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace WebAPI.Entity.Concrete
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
