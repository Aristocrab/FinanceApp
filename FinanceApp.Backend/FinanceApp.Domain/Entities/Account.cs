namespace FinanceApp.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Transaction> Transactions { get; set; } = null!;
    public User User { get; set; } = null!;
}