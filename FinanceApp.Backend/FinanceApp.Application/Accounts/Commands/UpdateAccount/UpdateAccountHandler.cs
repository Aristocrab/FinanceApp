using FinanceApp.Application.Database;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<UpdateAccountCommand> _validator;

    public UpdateAccountHandler(FinanceAppDbContext dbContext, IValidator<UpdateAccountCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Guid> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
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
            .FirstOrDefault(x => x.Id == request.AccountId);
        if (account is null)
        {
            throw new NotFoundException(nameof(Accounts), request.AccountId);
        }

        account.Name = request.Name;
        account.Balance = request.Balance;
        account.Currency = request.Currency;
        account.Icon = request.Icon;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}