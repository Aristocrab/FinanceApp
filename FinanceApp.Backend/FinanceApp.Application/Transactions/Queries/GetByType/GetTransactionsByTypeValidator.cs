using FluentValidation;

namespace FinanceApp.Application.Transactions.Queries.GetByType;

public class GetTransactionsByTypeValidator : AbstractValidator<GetTransactionsByTypeQuery>
{
    public GetTransactionsByTypeValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
    }
}