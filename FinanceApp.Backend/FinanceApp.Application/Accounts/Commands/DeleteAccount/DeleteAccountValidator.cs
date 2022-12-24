using FluentValidation;

namespace FinanceApp.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.AccountId)
            .NotEqual(Guid.Empty);
    }
}