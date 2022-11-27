using MediatR;

namespace FinanceApp.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public Guid CategoryId { get; set; }
}