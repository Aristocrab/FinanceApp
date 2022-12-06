using FinanceApp.Domain.Exceptions;
using MediatR;

namespace FinanceApp.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public UpdateAccountHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _dbContext.Accounts.FirstOrDefault(x => x.Id == request.AccountId);
        if (account is null)
        {
            throw new NotFoundException(nameof(Accounts), request.AccountId);
        }

        account.Name = request.Name;
        account.Balance = request.Balance;
        account.Currency = request.Currency;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}