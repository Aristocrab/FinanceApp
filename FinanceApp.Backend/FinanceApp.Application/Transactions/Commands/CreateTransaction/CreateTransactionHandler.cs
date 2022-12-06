using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;
using FinanceApp.Domain.Exceptions;
using MediatR;

namespace FinanceApp.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public CreateTransactionHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
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

        var transaction = new Transaction
        {
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            Type = request.Type,
            Category = category,
            Account = account
        };

        switch (transaction.Type)
        {
            case TransactionType.Expense:
                account.Balance -= transaction.Amount;
                break;
            case TransactionType.Income:
                account.Balance += transaction.Amount;
                break;
            default:
                throw new Exception();
        }

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}