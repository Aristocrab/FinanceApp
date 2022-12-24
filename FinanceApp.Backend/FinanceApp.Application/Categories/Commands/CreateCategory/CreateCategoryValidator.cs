using FluentValidation;

namespace FinanceApp.Application.Categories.Commands.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(128);
    }
}