using MediatR;

namespace FinanceApp.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Guid>
{
    public Guid CategoryId { get; set; }
    
    public string Name { get; set; } = null!;
}