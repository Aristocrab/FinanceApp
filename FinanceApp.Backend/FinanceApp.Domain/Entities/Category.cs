using FinanceApp.Domain.Entities.Base;

namespace FinanceApp.Domain.Entities;

public class Category : Entity
{
    public required string Name { get; set; }

    public List<Transaction> Transactions { get; set; } = null!;
    public required User User { get; set; }
}