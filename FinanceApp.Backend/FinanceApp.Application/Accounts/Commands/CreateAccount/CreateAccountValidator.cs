using FluentValidation;

namespace FinanceApp.Application.Accounts.Commands.CreateAccount;

public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(128);
        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.Currency)
            .IsInEnum();
        RuleFor(x => x.Icon)
            .GreaterThanOrEqualTo(0);
    }
}