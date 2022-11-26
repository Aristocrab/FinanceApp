using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
using MediatR;

namespace FinanceApp.Application.Accounts.Commands.CreateAccount;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public CreateAccountHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.UserId);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        var account = new Account
        {
            Name = request.Name,
            User = user
        };

        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return account.Id;
    }
}