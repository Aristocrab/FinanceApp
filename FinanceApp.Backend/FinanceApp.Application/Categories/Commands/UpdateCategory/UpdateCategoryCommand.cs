using MediatR;

namespace FinanceApp.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Guid>
{
    public required Guid UserId { get; set; }
    public required Guid CategoryId { get; set; }
    
    public required string Name { get; set; }
}