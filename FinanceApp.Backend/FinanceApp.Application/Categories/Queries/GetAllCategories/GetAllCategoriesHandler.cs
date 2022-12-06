using FinanceApp.Domain.Entities;
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
        // var user = _dbContext.Users
        //     .Include(x => x.Accounts)
        //     .FirstOrDefault(x => x.Id == request.UserId);
        // if (user is null)
        // {
        //     throw new NotFoundException(nameof(User), request.UserId);
        // }
        
        TypeAdapterConfig<Category, CategoryDto>
            .NewConfig()
            .Map(dest => dest.TransactionsCount,
                src => src.Transactions.Count);

        return Task.FromResult(
            _dbContext.Categories
                .Include(x => x.Transactions)
                .Adapt<List<CategoryDto>>()
            );
    }
}