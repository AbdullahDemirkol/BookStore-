using FluentValidation;
using WebAPI.Application.AuthorOperations.Commands.CommandHandler;

namespace WebAPI.Application.AuthorOperations.Commands.Validator
{
    public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).NotEmpty().GreaterThan(0);
        }
    }
}
