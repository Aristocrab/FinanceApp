using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public UpdateTransactionHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        // var user = _dbContext.Users
        //     .Include(x => x.Transactions)
        //     .ThenInclude(x => x.Category)
        //     .Include(x => x.Transactions)
        //     .ThenInclude(x => x.Account)
        //     .FirstOrDefault(x => x.Id == request.UserId);
        // if (user is null)
        // {
        //     throw new NotFoundException(nameof(User), request.UserId);
        // }

        var transaction = _dbContext.Transactions
            .FirstOrDefault(x => x.Id == request.TransactionId);
        if (transaction is null)
        {
            throw new NotFoundException(nameof(Transactions), request.TransactionId);
        }
        
        var category = _dbContext.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
        if (category is null && transaction.Type != TransactionType.Transfer)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }
        
        var account = _dbContext.Accounts.FirstOrDefault(x => x.Id == request.AccountId);
        if (account is null)
        {
            throw new NotFoundException(nameof(Account), request.AccountId);
        }

        if (transaction.Type == TransactionType.Expense)
        {
            account.Balance += transaction.Amount;
        }
        else if (transaction.Type == TransactionType.Income)
        {
            account.Balance -= transaction.Amount;
        }
        
        if (request.Type == TransactionType.Expense)
        {
            account.Balance -= request.Amount;
        }
        else if (request.Type == TransactionType.Income)
        {
            account.Balance += request.Amount;
        }

        transaction.Description = request.Description;
        transaction.Amount = request.Amount;
        transaction.Date = request.Date;
        transaction.Type = request.Type;
        // transaction.User = user;
        transaction.Category = category;
        transaction.Account = account;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}