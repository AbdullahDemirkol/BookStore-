using FluentValidation;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;

namespace WebAPI.Application.GenreOperations.Commands.Validator
{
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
        }
    }
}
