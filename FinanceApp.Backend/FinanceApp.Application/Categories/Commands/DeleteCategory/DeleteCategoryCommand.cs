using MediatR;

namespace FinanceApp.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public required Guid UserId { get; set; }
    public required Guid CategoryId { get; set; }
}