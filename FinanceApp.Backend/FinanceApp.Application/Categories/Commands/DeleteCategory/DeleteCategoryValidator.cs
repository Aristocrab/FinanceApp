using FluentValidation;

namespace FinanceApp.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty);
    }
}