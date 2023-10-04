using FluentValidation;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;

namespace WebAPI.Application.GenreOperations.Commands.Validator
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Name).NotNull().MinimumLength(4);
        }
    }
}
