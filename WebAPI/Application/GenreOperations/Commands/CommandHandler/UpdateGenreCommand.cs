using WebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.GenreOperations.Commands.CommandHandler
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.FirstOrDefault(g => g.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }
            bool isFoundGenre = _dbContext.Genres.Any(g => g.Name.ToLower().Replace(" ","") == Model.Name.ToLower().Replace(" ", "") 
                && g.Id!=GenreId);
            if (isFoundGenre)
            {
                throw new InvalidOperationException("Bu Isimde Tür Bulunmaktadır");
            }
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name  ;
            genre.IsActive = Model.IsActive;
            _dbContext.SaveChanges();
            Model.Name = genre.Name;
        }
    }
}
