using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Transactions.Queries.GetAll;

public class GetAllTransactionsHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionDto>>
{
    private readonly FinanceAppDbContext _dbContext;

    public GetAllTransactionsHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dbContext.Transactions
            .Include(x => x.Category)
            .Adapt<List<TransactionDto>>());
    }
}