using FluentValidation;
using WebAPI.Application.BookOperations.Queries.QueryHandler.GetBook;

namespace WebAPI.Application.BookOperations.Queries.Validator
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty().GreaterThan(0);
        }
    }
}
