using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Categories.Queries.GetCategoriesStats;

public class GetCategoriesStats : IRequest<List<CategoryStatsDto>>
{
    public TransactionType Type { get; set; }
    public Guid? AccountId { get; set; }
}