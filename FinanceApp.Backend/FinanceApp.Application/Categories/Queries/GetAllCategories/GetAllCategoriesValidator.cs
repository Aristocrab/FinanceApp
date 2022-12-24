using FinanceApp.Application.Accounts.Queries.GetAllAccounts;
using FluentValidation;

namespace FinanceApp.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesValidator : AbstractValidator<GetAllAccountsQuery>
{
    public GetAllCategoriesValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
    }
}