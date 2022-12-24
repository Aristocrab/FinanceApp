using FinanceApp.Application.Database;
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
            .Include(x=> x.User)
            .Include(x => x.Category)
            .Include(x => x.Account)
            .Where(x => x.User.Id == request.UserId)
            
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.TimeCreated)
            .Adapt<List<TransactionDto>>());
    }
}