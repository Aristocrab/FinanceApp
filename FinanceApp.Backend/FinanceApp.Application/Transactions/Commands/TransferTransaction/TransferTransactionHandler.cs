using FinanceApp.Application.Database;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace FinanceApp.Application.Transactions.Commands.TransferTransaction;

public class TransferTransactionHandler : IRequestHandler<TransferTransactionCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<TransferTransactionCommand> _validator;

    public TransferTransactionHandler(FinanceAppDbContext dbContext, IValidator<TransferTransactionCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Guid> Handle(TransferTransactionCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        if(request.AccountFromId == request.AccountToId)
        {
            throw new ForbiddenException();
        }
        
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.UserId);
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
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
            Account = accountFrom,
            User = user
        };

        accountFrom.Balance -= request.Amount;
        accountTo.Balance += request.Amount;

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}