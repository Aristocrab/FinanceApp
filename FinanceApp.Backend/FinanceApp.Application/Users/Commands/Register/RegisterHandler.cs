using FinanceApp.Application.Common.Utils;
using FinanceApp.Domain.Entities;
using MediatR;

namespace FinanceApp.Application.Users.Commands.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public RegisterHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var salt = PasswordHasher.GenerateSalt();
        var passwordHash = PasswordHasher.HashPassword(request.Password, salt);

        if (_dbContext.Users.Any(x => x.Email == request.Email || x.Username == request.Username))
        {
            throw new Exception(); // todo
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            Salt = salt
        };
        
        _dbContext.Users.Add(user);
        _dbContext.SaveChangesAsync(cancellationToken);
        
        return Task.FromResult(user.Id);
    }
}