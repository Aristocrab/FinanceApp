using FinanceApp.Domain.Exceptions;
using MediatR;

namespace FinanceApp.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public UpdateCategoryHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _dbContext.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
        if (category is null)
        {
            throw new NotFoundException(nameof(Accounts), request.CategoryId);
        }

        category.Name = request.Name;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}