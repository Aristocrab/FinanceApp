using MediatR;

namespace FinanceApp.Application.Transactions.Queries.GetAll;

public class GetAllTransactionsQuery : IRequest<List<TransactionDto>>
{
}