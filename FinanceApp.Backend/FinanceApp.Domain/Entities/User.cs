namespace FinanceApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public List<Transaction> Transactions { get; set; } = null!;
    public List<Account> Accounts { get; set; } = null!;
}