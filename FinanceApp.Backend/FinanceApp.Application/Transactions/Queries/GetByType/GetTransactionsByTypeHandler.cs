using FinanceApp.Application.Transactions.Queries.GetAll;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Transactions.Queries.GetByType;

public class GetTransactionsByTypeHandler : IRequestHandler<GetTransactionsByTypeQuery, List<TransactionDto>>
{
    private readonly FinanceAppDbContext _dbContext;

    public GetTransactionsByTypeHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<TransactionDto>> Handle(GetTransactionsByTypeQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dbContext.Transactions
            .Include(x => x.Category)
            .Where(x => x.Type == request.Type)
            .Adapt<List<TransactionDto>>());
    }
}