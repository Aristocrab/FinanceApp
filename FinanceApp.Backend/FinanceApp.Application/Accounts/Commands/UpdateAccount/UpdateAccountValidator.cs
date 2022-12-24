using FluentValidation;

namespace FinanceApp.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.AccountId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(128);
        RuleFor(x => x.Currency)
            .IsInEnum();
        RuleFor(x => x.Icon)
            .GreaterThanOrEqualTo(0);
    }
}