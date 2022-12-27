using FluentValidation;

namespace FinanceApp.Application.Users.Commands.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(32);
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(64);
    }
}