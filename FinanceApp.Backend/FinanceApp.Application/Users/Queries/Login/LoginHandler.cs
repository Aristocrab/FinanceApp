using FinanceApp.Application.Common.Utils;
using MediatR;

namespace FinanceApp.Application.Users.Queries.Login;

public class LoginHandler : IRequestHandler<LoginQuery, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public LoginHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Guid> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == request.Email);
        if (user is null)
        {
            throw new Exception(); // todo
            // throw new NotFoundException(nameof(User))
        }

        if (!PasswordHasher.ValidatePassword(request.Password, user.Salt, user.PasswordHash))
        {
            throw new Exception(); // todo
            // throw new UnauthorizedException();
        }

        return Task.FromResult(user.Id);
    }
}