using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Categories.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly FinanceAppDbContext _dbContext;

    public CreateCategoryHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users
            .Include(x => x.Categories)
            .FirstOrDefault(x => x.Id == request.UserId);

        if (user is null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        var category = new Category
        {
            Name = request.Name,
        };

        user.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}