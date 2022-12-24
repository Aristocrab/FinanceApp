using FluentValidation;

namespace FinanceApp.Application.Transactions.Queries.GetAll;

public class GetAllTransactionsValidator : AbstractValidator<GetAllTransactionsQuery>
{
    public GetAllTransactionsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
    }
}