namespace FinanceApp.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Transaction> Transactions { get; set; } = null!;
}