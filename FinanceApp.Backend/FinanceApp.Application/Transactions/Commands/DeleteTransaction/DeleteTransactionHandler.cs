using FinanceApp.Domain.Enums;
using FinanceApp.Domain.Exceptions;
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
        var transaction = _dbContext.Transactions
            .Include(x => x.Account)
            .FirstOrDefault(x => x.Id == request.TransactionId);
        if (transaction is null)
        {
            throw new NotFoundException(nameof(Transactions), request.TransactionId);
        }

        switch (transaction.Type)
        {
            case TransactionType.Expense:
                transaction.Account.Balance += transaction.Amount;
                break;
            case TransactionType.Income:
                transaction.Account.Balance -= transaction.Amount;
                break;
        }

        _dbContext.Transactions.Remove(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}