using FinanceApp.Domain.Enums;

namespace FinanceApp.WebApi.Models.Transactions;

public class TransferTransactionDto
{
    public Guid CategoryId { get; set; }
    public Guid AccountFromId { get; set; }
    public Guid AccountToId { get; set; }
    
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
}