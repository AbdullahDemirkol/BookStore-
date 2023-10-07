using FluentValidation;
using WebAPI.Application.UserOperations.Commands.CommandHandler;

namespace WebAPI.Application.UserOperations.Commands.Validator
{
    public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(command=>command.RefreshToken).NotEmpty();
        }
    }
}
