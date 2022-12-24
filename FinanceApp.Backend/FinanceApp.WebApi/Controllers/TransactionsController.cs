using FinanceApp.Application.Transactions.Commands.CreateTransaction;
using FinanceApp.Application.Transactions.Commands.DeleteTransaction;
using FinanceApp.Application.Transactions.Commands.TransferTransaction;
using FinanceApp.Application.Transactions.Commands.UpdateTransaction;
using FinanceApp.Application.Transactions.Queries.GetAll;
using FinanceApp.Application.Transactions.Queries.GetTransactionsStats;
using FinanceApp.WebApi.Controllers.Shared;
using FinanceApp.WebApi.Models.Transactions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.WebApi.Controllers;

[Authorize]
public class TransactionsController : BaseController
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public Task<List<TransactionDto>> GetAll()
    {
        return _mediator.Send(new GetAllTransactionsQuery
        {
            UserId = UserId
        });
    }
    
    [HttpGet("stats/{period}")]
    public Task<List<TransactionStatsDto>> GetTransactionsStats(TimePeriod period)
    {
        return _mediator.Send(new GetTransactionsStatsQuery
        {
            UserId = UserId,
            Period = period
        });
    }
    
    
    [HttpPost("new")]
    public Task<Guid> CreateTransaction([FromBody] CreateTransactionDto createTransactionDto)
    {
        var command = createTransactionDto.Adapt<CreateTransactionCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
    
    [HttpPost("transfer")]
    public Task<Guid> TransferTransaction([FromBody] TransferTransactionDto transferTransactionDto)
    {
        var command = transferTransactionDto.Adapt<TransferTransactionCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
    
    [HttpPut("update")]
    public Task<Guid> UpdateTransaction([FromBody] UpdateTransactionDto updateTransactionDto)
    {
        var command = updateTransactionDto.Adapt<UpdateTransactionCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
    
    [HttpDelete("delete")]
    public Task DeleteCategory([FromBody] DeleteTransactionDto deleteTransactionDto)
    {
        var command = deleteTransactionDto.Adapt<DeleteTransactionCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
}