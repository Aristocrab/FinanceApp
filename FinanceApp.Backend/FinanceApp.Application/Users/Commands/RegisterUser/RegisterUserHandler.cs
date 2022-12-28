using FinanceApp.Application.Database;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace FinanceApp.Application.Users.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, User>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly RegisterUserValidator _validator;

    public RegisterUserHandler(FinanceAppDbContext dbContext, RegisterUserValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        if (_dbContext.Users.Any(x => x.Username == request.Username))
        {
            throw new UserNotFoundException(request.Username);
        }

        var user = new User
        {
            Username = request.Username,
            Password = request.Password
        };
        _dbContext.Users.Add(user);
        
        var accounts = new[]
        {
            new Account
            {
                Name = "Cash",
                Balance = 0,
                Currency = Currency.UAH,
                Icon = 0,
                User = user
            },
            new Account
            {
                Name = "Debit card",
                Balance = 0, 
                Currency = Currency.USD,
                Icon = 1,
                User = user
            }
        };
        _dbContext.Accounts.AddRange(accounts);

        var categories = new[]
        {
            new Category
            {
                Name = "Food",
                User = user
            },
            new Category
            {
                Name = "Cafe",
                User = user
            },
            new Category
            {
                Name = "Car",
                User = user
            },
            new Category
            {
                Name = "Family",
                User = user
            }
        };
        _dbContext.Categories.AddRange(categories);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }
}