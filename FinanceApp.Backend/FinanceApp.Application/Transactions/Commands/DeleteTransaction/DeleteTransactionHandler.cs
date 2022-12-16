using FinanceApp.Application.Database;
using FinanceApp.Domain.Enums;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, Unit>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<DeleteTransactionCommand> _validator;

    public DeleteTransactionHandler(FinanceAppDbContext dbContext, IValidator<DeleteTransactionCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
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