using FinanceApp.Application.Database;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace FinanceApp.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<CreateTransactionCommand> _validator;

    public CreateTransactionHandler(FinanceAppDbContext dbContext, IValidator<CreateTransactionCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
        if (user is null)
        {
            throw new UserNotFoundException();
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

        var transaction = new Transaction
        {
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            Type = request.Type,
            User = user,
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
        }

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}