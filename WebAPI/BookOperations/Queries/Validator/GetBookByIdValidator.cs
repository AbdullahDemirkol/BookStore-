using FluentValidation;
using WebAPI.BookOperations.Queries.QueriesHandler.GetBook;

namespace WebAPI.BookOperations.Queries.Validator
{
    public class GetBookByIdValidator:AbstractValidator<GetBookById>
    {
        public GetBookByIdValidator()
        {
            RuleFor(query=>query.Id).NotEmpty().GreaterThan(0);
        }
    }
}
