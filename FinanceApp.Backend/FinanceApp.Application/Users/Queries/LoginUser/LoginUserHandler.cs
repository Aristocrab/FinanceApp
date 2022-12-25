using FinanceApp.Application.Database;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace FinanceApp.Application.Users.Queries.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, User>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly LoginUserValidator _validator;

    public LoginUserHandler(FinanceAppDbContext dbContext, LoginUserValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<User> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        var user = _dbContext.Users.FirstOrDefault(x => x.Username == request.Username);
        if (user is null)
        {
            throw new UserNotFoundException(request.Username);
        }
        if (user.Password != request.Password)
        {
            throw new PasswordIncorrectException();
        }
        
        return user;
    }
}