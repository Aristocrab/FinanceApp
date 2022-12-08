using FluentValidation;

namespace FinanceApp.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.AccountId)
            .NotEqual(Guid.Empty);
        
        RuleFor(x => x.Amount)
            .GreaterThan(0);
        RuleFor(x => x.Description)
            .NotEmpty().MaximumLength(256);
        RuleFor(x => x.Type)
            .IsInEnum();
        RuleFor(x => x.Date)
            .Must(date => date != default);
    }
}