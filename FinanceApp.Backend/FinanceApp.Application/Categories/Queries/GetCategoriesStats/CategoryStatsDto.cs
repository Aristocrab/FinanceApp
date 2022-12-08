using FinanceApp.Application.Categories.Queries.GetAllCategories;

namespace FinanceApp.Application.Categories.Queries.GetCategoriesStats;

public class CategoryStatsDto
{
    public CategoryDto Category { get; set; } = null!;
    public int Count { get; set; }
}