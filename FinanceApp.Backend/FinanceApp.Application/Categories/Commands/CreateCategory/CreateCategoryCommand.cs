using MediatR;

namespace FinanceApp.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
}