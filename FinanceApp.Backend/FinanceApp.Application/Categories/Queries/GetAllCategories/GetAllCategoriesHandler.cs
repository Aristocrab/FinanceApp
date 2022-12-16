using FinanceApp.Application.Database;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    private readonly FinanceAppDbContext _dbContext;

    public GetAllCategoriesHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _dbContext.Categories
                .Include(x => x.Transactions)
                .OrderBy(x => x.TimeCreated)
                .Adapt<List<CategoryDto>>()
            );
    }
}