using FluentValidation;

namespace FinanceApp.Application.Transactions.Queries.GetTransactionsStats;

public class GetTransactionsStatsValidator : AbstractValidator<GetTransactionsStatsQuery>
{
    public GetTransactionsStatsValidator()
    {
        RuleFor(x => x.Period)
            .IsInEnum();
    }
}