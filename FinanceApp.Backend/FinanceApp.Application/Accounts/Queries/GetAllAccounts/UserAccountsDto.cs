namespace FinanceApp.Application.Accounts.Queries.GetAllAccounts;

public class UserAccountsDto
{
    public required decimal AccountsBalanceSum { get; set; }
    public required List<AccountDto> Accounts { get; set; }
}