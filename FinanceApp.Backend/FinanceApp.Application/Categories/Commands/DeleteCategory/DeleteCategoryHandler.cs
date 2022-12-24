using FinanceApp.Application.Database;
using FinanceApp.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<DeleteCategoryCommand> _validator;

    public DeleteCategoryHandler(FinanceAppDbContext dbContext, IValidator<DeleteCategoryCommand> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
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
            .Include(x => x.Transactions)
            .FirstOrDefault(x => x.Id == request.CategoryId);
        if (category is null)
        {
            throw new NotFoundException(nameof(Accounts), request.CategoryId);
        }

        if (category.Transactions.Any())
        {
            throw new Exception();
        }

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}