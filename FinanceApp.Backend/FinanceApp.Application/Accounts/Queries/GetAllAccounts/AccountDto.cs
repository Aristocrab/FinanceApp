namespace FinanceApp.Application.Accounts.Queries.GetAllAccounts;

public class AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}