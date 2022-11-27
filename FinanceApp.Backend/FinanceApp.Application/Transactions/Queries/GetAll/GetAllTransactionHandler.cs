using FinanceApp.Application.Common.Exceptions;
using FinanceApp.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Application.Transactions.Queries.GetAll;

public class GetAllTransactionHandler : IRequestHandler<GetAllTransactionQuery, List<TransactionDto>>
{
    private readonly FinanceAppDbContext _dbContext;

    public GetAllTransactionHandler(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<TransactionDto>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
    {
        // var user = _dbContext.Users
        //     .Include(x => x.Transactions)
        //     .FirstOrDefault(x => x.Id == request.UserId);
        // if (user is null)
        // {
        //     throw new NotFoundException(nameof(User), request.UserId);
        // }

        return Task.FromResult(_dbContext.Transactions.Adapt<List<TransactionDto>>());
    }
}