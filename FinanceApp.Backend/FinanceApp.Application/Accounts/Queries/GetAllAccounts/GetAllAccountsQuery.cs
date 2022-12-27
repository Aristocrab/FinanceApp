using MediatR;

namespace FinanceApp.Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsQuery : IRequest<UserAccountsDto>
{
    public required Guid UserId { get; set; }
}