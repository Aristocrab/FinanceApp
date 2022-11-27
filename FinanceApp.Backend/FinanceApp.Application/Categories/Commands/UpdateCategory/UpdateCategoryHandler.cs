using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        // var user = _dbContext.Users
        //     .Include(x => x.Categories)
        //     .FirstOrDefault(x => x.Id == request.UserId);
        // if (user is null)
        // {
        //     throw new NotFoundException(nameof(User), request.UserId);
        // }

        var category = _dbContext.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
        if (category is null)
        {
            throw new NotFoundException(nameof(Accounts), request.CategoryId);
        }

        // if (!user.Categories.Contains(category))
        // {
        //     throw new UnauthorizedException(user, nameof(Account), category.Id);
        // }

        category.Name = request.Name;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}