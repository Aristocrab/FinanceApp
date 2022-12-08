using MediatR;

namespace FinanceApp.Application.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionCommand : IRequest<Unit>
{
    public Guid TransactionId { get; set; }
}