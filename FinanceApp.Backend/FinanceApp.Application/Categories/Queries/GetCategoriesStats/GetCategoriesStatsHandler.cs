using FinanceApp.Application.Categories.Queries.GetAllCategories;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Categories.Queries.GetCategoriesStats;

public class GetCategoriesStatsHandler : IRequestHandler<GetCategoriesStats, List<CategoryStatsDto>>
{
    private readonly FinanceAppDbContext _dbContext;

    public GetCategoriesStatsHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CategoryStatsDto>> Handle(GetCategoriesStats request, CancellationToken cancellationToken)
    {
        var transactions = _dbContext.Transactions
            .Include(x => x.Category)
            .Where(x => x.Type == request.Type);

        if (request.AccountId is not null)
        {
            transactions = transactions.Include(x => x.Account)
                .Where(x => x.Account.Id == request.AccountId);
        }
            
        var stats = transactions
            .OrderBy(x => x.Category!.Name)
            .GroupBy(x => x.Category)
            .Select(x => new CategoryStatsDto
            {
                Category = x.First().Category!.Adapt<CategoryDto>(),
                Count = x.Select(y => y).Count()
            });

        return await stats.ToListAsync(cancellationToken: cancellationToken);
    }
}