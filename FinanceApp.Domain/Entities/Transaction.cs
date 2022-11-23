using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public TransactionType Type { get; set; }
    public Category Category { get; set; }
    public List<Tag> Tags { get; set; }
    public DateTime Date { get; set; }
    
    public User User { get; set; }
}