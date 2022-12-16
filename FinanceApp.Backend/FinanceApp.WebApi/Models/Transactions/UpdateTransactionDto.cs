using FinanceApp.Domain.Enums;

namespace FinanceApp.WebApi.Models.Transactions;

public class UpdateTransactionDto
{
    public Guid TransactionId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AccountId { get; set; }
    
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
}