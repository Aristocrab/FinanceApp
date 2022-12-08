using FinanceApp.Domain.Entities.Base;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Transaction : Entity
{
    public required decimal Amount { get; set; }
    public required string Description { get; set; }
    public required TransactionType Type { get; set; }
    public required DateTime Date { get; set; }

    public Category? Category { get; set; }
    public required Account Account { get; set; }
}