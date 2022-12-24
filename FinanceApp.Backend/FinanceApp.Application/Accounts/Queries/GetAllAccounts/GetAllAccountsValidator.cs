using FluentValidation;

namespace FinanceApp.Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsValidator : AbstractValidator<GetAllAccountsQuery>
{
    public GetAllAccountsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty);
    }
}