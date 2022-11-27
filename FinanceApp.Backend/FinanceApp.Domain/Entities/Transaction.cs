using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    
    public Category Category { get; set; } = null!;
    public Account Account { get; set; } = null!;
    // public User User { get; set; } = null!;
}