using MediatR;

namespace FinanceApp.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    
    public string Name { get; set; } = null!;
}