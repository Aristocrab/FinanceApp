using FluentValidation;

namespace FinanceApp.Application.Transactions.Queries.GetTransactionsStats;

public class GetTransactionsStatsValidator : AbstractValidator<GetTransactionsStatsQuery>
{
    public GetTransactionsStatsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.Period)
            .IsInEnum();
    }
}