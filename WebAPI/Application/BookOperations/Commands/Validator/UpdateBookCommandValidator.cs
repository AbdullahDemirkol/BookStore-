using FluentValidation;
using WebAPI.Application.BookOperations.Commands.CommandHandler;

namespace WebAPI.Application.BookOperations.Commands.Validator
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.PageCount).NotEmpty().GreaterThan(0);
        }
    }
}
