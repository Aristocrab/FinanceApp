using FluentValidation;

namespace FinanceApp.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionValidator : AbstractValidator<DeleteTransactionCommand>
{
    public DeleteTransactionValidator()
    {
        RuleFor(x => x.TransactionId)
            .NotEqual(Guid.Empty);
    }
}