using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, Unit>
{
    private readonly FinanceAppDbContext _dbContext;

    public DeleteTransactionHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users
            .Include(x => x.Accounts)
            .FirstOrDefault(x => x.Id == request.UserId);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        var transaction = user.Transactions.FirstOrDefault(x => x.Id == request.TransactionId);
        if (transaction is null)
        {
            throw new NotFoundException(nameof(Transactions), request.TransactionId);
        }

        if (!user.Transactions.Contains(transaction))
        {
            throw new UnauthorizedException(user, nameof(Transaction), transaction.Id);
        }

        _dbContext.Transactions.Remove(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}