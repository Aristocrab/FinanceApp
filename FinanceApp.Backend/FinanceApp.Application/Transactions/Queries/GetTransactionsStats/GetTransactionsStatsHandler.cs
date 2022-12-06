using System.Globalization;
using MediatR;

namespace FinanceApp.Application.Transactions.Queries.GetTransactionsStats;

public class GetTransactionsStatsHandler : IRequestHandler<GetTransactionsStatsQuery, List<TransactionStatsDto>>
{
    private readonly FinanceAppDbContext _dbContext;

    public GetTransactionsStatsHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TransactionStatsDto>> Handle(GetTransactionsStatsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<TransactionStatsDto> transactionStats = null!;
        
        switch (request.Period)
        {
            case TimePeriod.Day:
                transactionStats = _dbContext.Transactions
                    .AsEnumerable()
                    .Where(x => DateTime.Now - x.Date <= TimeSpan.FromDays(30))
                    .OrderBy(x => x.Date)
                    .GroupBy(x => $"{x.Date.Day:D2}.{x.Date.Month:D2}")
                    .Select(x => new TransactionStatsDto
                    {
                        TimePeriod = x.Key,
                        Amount = x.Sum(transaction => transaction.Amount)
                    });
                break;
            case TimePeriod.Month:
                transactionStats = _dbContext.Transactions
                    .AsEnumerable()
                    .Where(x => DateTime.Now - x.Date <= TimeSpan.FromDays(365))
                    .OrderBy(x => x.Date)
                    .GroupBy(x => x.Date.Month)
                    .Select(x => new TransactionStatsDto
                    {
                        TimePeriod = new DateTime(1, x.Key, 1)
                            .ToString("MMMM", CultureInfo.InvariantCulture),
                        Amount = x.Sum(transaction => transaction.Amount)
                    });
                break;
        }

        return transactionStats.ToList();
    }
}