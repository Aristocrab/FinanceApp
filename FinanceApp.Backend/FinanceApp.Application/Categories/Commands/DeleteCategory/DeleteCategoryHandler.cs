using FinanceApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly FinanceAppDbContext _dbContext;

    public DeleteCategoryHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _dbContext.Categories
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