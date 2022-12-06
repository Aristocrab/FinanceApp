using FinanceApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly FinanceAppDbContext _dbContext;

    public DeleteAccountHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _dbContext.Accounts
            .Include(x => x.Transactions)
            .FirstOrDefault(x => x.Id == request.AccountId);
        if (account is null)
        {
            throw new NotFoundException(nameof(Accounts), request.AccountId);
        }

        if (account.Transactions.Any())
        {
            throw new Exception();
        }

        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}