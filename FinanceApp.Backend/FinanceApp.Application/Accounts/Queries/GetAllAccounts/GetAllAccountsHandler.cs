using FinanceApp.Application.Common.Helpers;
using FinanceApp.Application.Database;
using FinanceApp.Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsHandler : IRequestHandler<GetAllAccountsQuery, UserAccountsDto>
{
    private readonly FinanceAppDbContext _dbContext;

    public GetAllAccountsHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<UserAccountsDto> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = _dbContext.Accounts
            .Include(x => x.Transactions)
            .Include(x => x.User)
            .Where(x => x.User.Id == request.UserId)
            .OrderBy(x => x.TimeCreated)
            .ToList();

        return Task.FromResult(new UserAccountsDto
        {
            Accounts = accounts.Adapt<List<AccountDto>>(),
            AccountsBalanceSum = accounts
                .Sum(x => ChangeCurrencyHelper.ChangeCurrency(x.Balance, x.Currency, Currency.UAH))
        });
    }
}