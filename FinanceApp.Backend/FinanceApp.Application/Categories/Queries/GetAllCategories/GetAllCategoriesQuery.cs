using MediatR;

namespace FinanceApp.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
{
    public required Guid UserId { get; set; }
}