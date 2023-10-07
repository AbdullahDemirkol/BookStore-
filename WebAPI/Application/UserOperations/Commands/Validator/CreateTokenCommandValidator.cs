using FluentValidation;
using WebAPI.Application.UserOperations.Commands.CommandHandler;

namespace WebAPI.Application.UserOperations.Commands.Validator
{
    public class CreateTokenCommandValidator:AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(command => command.Model.Email).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(4);
        }
    }
}
