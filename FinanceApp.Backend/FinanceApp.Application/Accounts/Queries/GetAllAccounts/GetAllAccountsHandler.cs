using FinanceApp.Application.Database;
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
        return _dbContext.Accounts
            .Include(x => x.User)
            .Where(x => x.User.Id == request.UserId)
            .Include(x => x.Transactions)
            .OrderBy(x => x.TimeCreated)
            .ProjectToType<AccountDto>()
            .ToListAsync(cancellationToken: cancellationToken);
    }
}