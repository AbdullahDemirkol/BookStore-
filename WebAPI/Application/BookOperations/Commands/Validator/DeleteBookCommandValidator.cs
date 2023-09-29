using FluentValidation;
using WebAPI.Application.BookOperations.Commands.CommandHandler;

namespace WebAPI.Application.BookOperations.Commands.Validator
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().GreaterThan(0);
        }
    }
}
