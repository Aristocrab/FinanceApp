using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Transactions.Commands.TransferTransaction;

public class TransferTransactionHandler : IRequestHandler<TransferTransactionCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public TransferTransactionHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(TransferTransactionCommand request, CancellationToken cancellationToken)
    {
        // var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
        // if (user is null)
        // {
        //     throw new NotFoundException(nameof(User), request.UserId);
        // }
        
        var accountFrom = _dbContext.Accounts.FirstOrDefault(x => x.Id == request.AccountFromId);
        if (accountFrom is null)
        {
            throw new NotFoundException(nameof(Account), request.AccountFromId);
        }
        
        var accountTo = _dbContext.Accounts.FirstOrDefault(x => x.Id == request.AccountToId);
        if (accountTo is null)
        {
            throw new NotFoundException(nameof(Account), request.AccountToId);
        }

        var transaction = new Transaction
        {
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            Type = TransactionType.Transfer,
            Account = accountFrom
        };

        accountFrom.Balance -= request.Amount;
        accountTo.Balance += request.Amount;

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}