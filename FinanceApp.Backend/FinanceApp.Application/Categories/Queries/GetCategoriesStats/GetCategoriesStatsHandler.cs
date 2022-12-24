using FinanceApp.Application.Categories.Queries.GetAllCategories;
using FinanceApp.Application.Database;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Categories.Queries.GetCategoriesStats;

public class GetCategoriesStatsHandler : IRequestHandler<GetCategoriesStatsQuery, List<CategoryStatsDto>>
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IValidator<GetCategoriesStatsQuery> _validator;

    public GetCategoriesStatsHandler(FinanceAppDbContext dbContext, IValidator<GetCategoriesStatsQuery> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<List<CategoryStatsDto>> Handle(GetCategoriesStatsQuery request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        var transactions = _dbContext.Transactions
            .Include(x => x.Category)
            .Where(x => x.Type == request.Type);

        if (request.AccountId is not null)
        {
            transactions = transactions.Include(x => x.Account)
                .Where(x => x.Account.Id == request.AccountId);
        }
            
        var stats = transactions
            .Include(x => x.User)
            .Where(x => x.User.Id == request.UserId)
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