using FluentValidation;

namespace FinanceApp.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(128);
    }
}