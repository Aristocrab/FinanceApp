using FinanceApp.Domain.Entities.Base;

namespace FinanceApp.Domain.Entities;

public class User : Entity
{
    public required string Username { get; set; }
    
    public required string Password { get; set; } // todo
    // public required string PasswordHash { get; set; }
    // public required string Salt { get; set; }
    
    public List<Transaction> Transactions { get; set; } = null!;
    public List<Account> Accounts { get; set; } = null!;
    public List<Category> Categories { get; set; } = null!;
}