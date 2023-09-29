using FluentValidation;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;

namespace WebAPI.Application.GenreOperations.Commands.Validator
{
    public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}
