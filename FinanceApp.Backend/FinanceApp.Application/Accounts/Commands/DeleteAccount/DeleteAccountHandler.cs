using FinanceApp.Application.Database;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<DeleteAccountCommand> _validator;

    public DeleteAccountHandler(FinanceAppDbContext dbContext, IValidator<DeleteAccountCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
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
        
        var account = _dbContext.Accounts
            .Include(x => x.User)
            .Where(x => x.User.Id == request.UserId)
            .Include(x => x.Transactions)
            .FirstOrDefault(x => x.Id == request.AccountId);
        if (account is null)
        {
            throw new NotFoundException(nameof(Accounts), request.AccountId);
        }

        if (account.Transactions.Any())
        {
            throw new Exception();
        }

        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}