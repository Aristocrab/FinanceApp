namespace FinanceApp.Application.Transactions.Queries.GetAll;

public class TransactionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}