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
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.WebApi.Controllers;

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
        return _mediator.Send(new GetAllTransactionsQuery());
    }
    
    [HttpGet("stats/{period}")]
    public Task<List<TransactionStatsDto>> GetTransactionsStats(TimePeriod period)
    {
        return _mediator.Send(new GetTransactionsStatsQuery
        {
            Period = period
        });
    }
    
    
    [HttpPost("new")]
    public Task<Guid> CreateTransaction([FromBody] CreateTransactionDto createTransactionDto)
    {
        return _mediator.Send(createTransactionDto.Adapt<CreateTransactionCommand>());
    }
    
    [HttpPost("transfer")]
    public Task<Guid> TransferTransaction([FromBody] TransferTransactionDto transferTransactionDto)
    {
        return _mediator.Send(transferTransactionDto.Adapt<TransferTransactionCommand>());
    }
    
    [HttpPut("update")]
    public Task<Guid> UpdateTransaction([FromBody] UpdateTransactionDto updateTransactionDto)
    {
        return _mediator.Send(updateTransactionDto.Adapt<UpdateTransactionCommand>());
    }
    
    [HttpDelete("delete")]
    public Task DeleteCategory([FromBody] DeleteTransactionDto deleteTransactionDto)
    {
        return _mediator.Send(deleteTransactionDto.Adapt<DeleteTransactionCommand>());
    }
}