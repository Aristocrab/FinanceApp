using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
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
        var user = _dbContext.Users
            .Include(x => x.Accounts)
            .FirstOrDefault(x => x.Id == request.UserId);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        var account = user.Accounts.FirstOrDefault(x => x.Id == request.AccountId);
        if (account is null)
        {
            throw new NotFoundException(nameof(Accounts), request.AccountId);
        }

        if (!user.Accounts.Contains(account))
        {
            throw new UnauthorizedException(user, nameof(Account), account.Id);
        }

        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}