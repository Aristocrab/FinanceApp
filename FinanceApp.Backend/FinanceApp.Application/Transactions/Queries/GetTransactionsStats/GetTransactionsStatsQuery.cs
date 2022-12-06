using MediatR;

namespace FinanceApp.Application.Transactions.Queries.GetTransactionsStats;

public class GetTransactionsStatsQuery : IRequest<List<TransactionStatsDto>>
{
    public TimePeriod Period { get; set; }
}