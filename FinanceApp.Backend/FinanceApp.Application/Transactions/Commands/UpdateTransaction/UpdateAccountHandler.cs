using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Transactions.Commands.UpdateTransaction;

public class UpdateAccountHandler : IRequestHandler<UpdateTransactionCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public UpdateAccountHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users
            .Include(x => x.Transactions)
            .ThenInclude(x => x.Category)
            .Include(x => x.Transactions)
            .ThenInclude(x => x.Account)
            .FirstOrDefault(x => x.Id == request.UserId);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }
        
        var category = _dbContext.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
        if (category is null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }
        
        var account = _dbContext.Accounts.FirstOrDefault(x => x.Id == request.AccountId);
        if (account is null)
        {
            throw new NotFoundException(nameof(Account), request.AccountId);
        }

        var transaction = user.Transactions
            .FirstOrDefault(x => x.Id == request.AccountId);
        if (transaction is null)
        {
            throw new NotFoundException(nameof(Transactions), request.TransactionId);
        }

        transaction.Description = request.Description;
        transaction.Amount = request.Amount;
        transaction.Date = request.Date;
        transaction.User = user;
        transaction.Category = category;
        transaction.Account = account;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}