namespace WebAPI.Application.GenreOperations.Commands.RequestCommandModel
{
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
