using FluentValidation;
using WebAPI.Application.BookOperations.Commands.CommandHandler;

namespace WebAPI.Application.BookOperations.Commands.Validator
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.AuthorId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.PageCount).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);

        }
    }
}
