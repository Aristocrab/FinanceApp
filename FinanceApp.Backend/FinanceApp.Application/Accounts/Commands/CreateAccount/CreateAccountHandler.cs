using FinanceApp.Domain.Entities;
using MediatR;

namespace FinanceApp.Application.Accounts.Commands.CreateAccount;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public CreateAccountHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Account
        {
            Name = request.Name,
            Balance = request.Balance,
            Currency = request.Currency
        };

        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}