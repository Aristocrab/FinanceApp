using FluentValidation;

namespace FinanceApp.Application.Categories.Queries.GetCategoriesStats;

public class GetCategoriesStatsValidator : AbstractValidator<GetCategoriesStatsQuery>
{
    public GetCategoriesStatsValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum();
        RuleFor(x => x.AccountId)
            .NotEqual(Guid.Empty);
    }
}