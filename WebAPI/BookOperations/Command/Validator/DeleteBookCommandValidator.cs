using FluentValidation;
using WebAPI.BookOperations.Command.CommandHandler;

namespace WebAPI.BookOperations.Command.Validator
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command=>command.Id).NotEmpty().GreaterThan(0);
        }
    }
}
