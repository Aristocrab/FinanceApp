using FinanceApp.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    private readonly FinanceAppDbContext _dbContext;

    public GetAllAccountsHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        TypeAdapterConfig<Account, AccountDto>
            .NewConfig()
            .Map(dest => dest.TransactionsCount,
                src => src.Transactions.Count);
        
        return _dbContext.Accounts
            .Include(x => x.Transactions)
            .ProjectToType<AccountDto>()
            .ToListAsync(cancellationToken: cancellationToken);
    }
}