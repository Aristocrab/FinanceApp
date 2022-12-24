using FinanceApp.Application.Database;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace FinanceApp.Application.Categories.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<CreateCategoryCommand> _validator;

    public CreateCategoryHandler(FinanceAppDbContext dbContext, IValidator<CreateCategoryCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
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
        
        var category = new Category
        {
            User = user,
            Name = request.Name,
        };

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}