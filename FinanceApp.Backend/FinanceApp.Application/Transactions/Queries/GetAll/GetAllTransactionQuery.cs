using MediatR;

namespace FinanceApp.Application.Transactions.Queries.GetAll;

public class GetAllTransactionQuery : IRequest<List<TransactionDto>>
{
    public Guid UserId { get; set; }
}