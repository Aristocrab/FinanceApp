using FluentValidation;

namespace FinanceApp.Application.Transactions.Commands.TransferTransaction;

public class TransferTransactionValidator : AbstractValidator<TransferTransactionCommand>
{
    public TransferTransactionValidator()
    {
        RuleFor(x => x.AccountFromId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.AccountToId)
            .NotEqual(Guid.Empty);
        
        RuleFor(x => x.Amount)
            .GreaterThan(0);
        RuleFor(x => x.Description)
            .NotEmpty().MaximumLength(256);
        RuleFor(x => x.Date)
            .Must(date => date != default);
    }
}