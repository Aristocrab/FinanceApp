using FinanceApp.Application.Database;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<UpdateTransactionCommand> _validator;

    public UpdateTransactionHandler(FinanceAppDbContext dbContext, IValidator<UpdateTransactionCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Guid> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
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
        
        var transaction = _dbContext.Transactions
            .Include(x => x.User)
            .FirstOrDefault(x => x.Id == request.TransactionId);
        if (transaction is null)
        {
            throw new NotFoundException(nameof(Transactions), request.TransactionId);
        }

        if (transaction.User.Id != request.UserId)
        {
            throw new UserNotFoundException();
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

        switch (transaction.Type)
        {
            case TransactionType.Expense:
                account.Balance += transaction.Amount;
                break;
            case TransactionType.Income:
                account.Balance -= transaction.Amount;
                break;
        }
        
        switch (request.Type)
        {
            case TransactionType.Expense:
                account.Balance -= request.Amount;
                break;
            case TransactionType.Income:
                account.Balance += request.Amount;
                break;
        }

        transaction.Description = request.Description;
        transaction.Amount = request.Amount;
        transaction.Date = request.Date;
        transaction.Type = request.Type;
        transaction.Category = category;
        transaction.Account = account;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}