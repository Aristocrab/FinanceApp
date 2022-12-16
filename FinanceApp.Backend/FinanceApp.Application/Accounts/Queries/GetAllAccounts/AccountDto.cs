namespace FinanceApp.Application.Accounts.Queries.GetAllAccounts;

public class AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public required decimal Balance { get; set; }
    public required string Currency { get; set; }
    public required int Icon { get; set; }
    
    public int TransactionsCount { get; set; }
}