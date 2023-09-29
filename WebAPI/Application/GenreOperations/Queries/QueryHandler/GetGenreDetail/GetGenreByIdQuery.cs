using AutoMapper;
using WebAPI.Application.GenreOperations.Queries.QueryViewModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre
{
    public class GetGenreByIdQuery
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GenreViewModel Handle()
        {
            var genre = _dbContext.Genres.FirstOrDefault(p=>p.IsActive && p.Id==GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }
            GenreViewModel genreViewModel = _mapper.Map<GenreViewModel>(genre);
            return genreViewModel;
        }

    }
}
