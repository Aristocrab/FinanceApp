namespace FinanceApp.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public List<Transaction> Transactions { get; set; } = null!;
}