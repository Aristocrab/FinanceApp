using MediatR;

namespace FinanceApp.Application.Transactions.Queries.GetTransactionsStats;

public class GetTransactionsStatsQuery : IRequest<List<TransactionStatsDto>>
{
    public required Guid UserId { get; set; }
    public TimePeriod Period { get; set; }
}