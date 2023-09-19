using FluentValidation;
using WebAPI.BookOperations.Command.CommandHandler;

namespace WebAPI.BookOperations.Command.Validator
{
    public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command=>command.Id).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.GenreId).NotEmpty();
            RuleFor(command => command.Model.PageCount).NotEmpty().GreaterThan(0);
        }
    }
}
