using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public required decimal Amount { get; set; }
    public required string Description { get; set; }
    public required TransactionType Type { get; set; }
    public required DateTime Date { get; set; }

    public Category? Category { get; set; }
    public required Account Account { get; set; }
    // public User User { get; set; } = null!;
}