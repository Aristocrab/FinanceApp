using FluentValidation;

namespace FinanceApp.Application.Users.Queries.LoginUser;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().MaximumLength(32);
        RuleFor(x => x.Password)
            .NotEmpty().MaximumLength(64);
    }
}