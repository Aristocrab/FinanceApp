using FinanceApp.Application.Database;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<UpdateCategoryCommand> _validator;

    public UpdateCategoryHandler(FinanceAppDbContext dbContext, IValidator<UpdateCategoryCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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
        
        var category = _dbContext.Categories
            .Include(x => x.User)
            .Where(x => x.User.Id == request.UserId)
            .FirstOrDefault(x => x.Id == request.CategoryId);
        if (category is null)
        {
            throw new NotFoundException(nameof(Accounts), request.CategoryId);
        }

        category.Name = request.Name;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}