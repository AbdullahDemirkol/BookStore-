using FluentValidation;
using WebAPI.Application.AuthorOperations.Queries.QueryHandler.GetAuthor;

namespace WebAPI.Application.AuthorOperations.Queries.Validator
{
    public class GetAuthorByIdQueryValidator:AbstractValidator<GetAuthorByIdQuery>
    {
        public GetAuthorByIdQueryValidator()
        {
            RuleFor(query => query.AuthorId).GreaterThan(0);
        }
    }
}
