using FinanceApp.Application.Transactions.Queries.GetAll;
using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Transactions.Queries.GetByType;

public class GetTransactionsByTypeQuery : IRequest<List<TransactionDto>>
{
    public Guid UserId { get; set; }
    public TransactionType Type { get; set; }
}