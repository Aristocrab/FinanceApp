using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Transactions.Queries.GetAll;

public class TransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}