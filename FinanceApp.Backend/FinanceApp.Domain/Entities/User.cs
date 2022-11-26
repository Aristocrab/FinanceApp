namespace FinanceApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Salt { get; set; } = null!;
    
    public List<Transaction> Transactions { get; set; } = null!;
    public List<Category> Categories { get; set; } = null!;
    public List<Account> Accounts { get; set; } = null!;
}