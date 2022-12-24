using MediatR;

namespace FinanceApp.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Guid>
{
    public required Guid UserId { get; set; }
    public required string Name { get; set; }
}