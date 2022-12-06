using FinanceApp.Application.Common.Exceptions;
using MediatR;

namespace FinanceApp.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public UpdateAccountHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        // var user = _dbContext.Users
        //     .Include(x => x.Accounts)
        //     .FirstOrDefault(x => x.Id == request.UserId);
        // if (user is null)
        // {
        //     throw new NotFoundException(nameof(User), request.UserId);
        // }

        var account = _dbContext.Accounts.FirstOrDefault(x => x.Id == request.AccountId);
        if (account is null)
        {
            throw new NotFoundException(nameof(Accounts), request.AccountId);
        }

        // if (!user.Accounts.Contains(account))
        // {
        //     throw new UnauthorizedException(user, nameof(Account), account.Id);
        // }

        account.Name = request.Name;
        account.Balance = request.Balance;
        account.Currency = request.Currency;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}