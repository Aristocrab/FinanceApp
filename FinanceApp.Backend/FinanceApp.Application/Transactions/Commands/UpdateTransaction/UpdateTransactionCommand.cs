using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommand : IRequest<Guid>
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }

    public Guid TransactionId { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AccountId { get; set; }
}