using FluentValidation;
using WebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;

namespace WebAPI.Application.GenreOperations.Queries.Validator
{
    public class GetGenreByIdQueryValidator:AbstractValidator<GetGenreByIdQuery>
    {
        public GetGenreByIdQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}
