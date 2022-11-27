﻿using FinanceApp.Application.Transactions.Commands.CreateTransaction;
using FinanceApp.Application.Transactions.Commands.DeleteTransaction;
using FinanceApp.Application.Transactions.Commands.UpdateTransaction;
using FinanceApp.Application.Transactions.Queries.GetAll;
using FinanceApp.WebApi.Controllers.Shared;
using FinanceApp.WebApi.Models.Transactions;
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
        return _mediator.Send(new GetAllTransactionQuery
        {
            UserId = Guid.Empty // todo
        });
    }
    
    [HttpPost("new")]
    public Task<Guid> CreateTransaction([FromBody] CreateTransactionDto createTransactionDto)
    {
        return _mediator.Send(new CreateTransactionCommand
        {
            UserId = Guid.Empty,
            AccountId = createTransactionDto.AccountId,
            Amount = createTransactionDto.Amount,
            CategoryId = createTransactionDto.CategoryId,
            Date = createTransactionDto.Date,
            Description = createTransactionDto.Description,
            Type = createTransactionDto.Type
        });
    }
    
    [HttpPut("update")]
    public Task<Guid> UpdateTransaction([FromBody] UpdateTransactionDto updateTransactionDto)
    {
        return _mediator.Send(new UpdateTransactionCommand
        {
            UserId = Guid.Empty,
            TransactionId = updateTransactionDto.TransactionId,
            AccountId = updateTransactionDto.AccountId,
            Amount = updateTransactionDto.Amount,
            CategoryId = updateTransactionDto.CategoryId,
            Date = updateTransactionDto.Date,
            Description = updateTransactionDto.Description,
            Type = updateTransactionDto.Type
        });
    }
    
    [HttpDelete("delete")]
    public Task DeleteCategory([FromBody] DeleteTransactionDto deleteTransactionDto)
    {
        return _mediator.Send(new DeleteTransactionCommand
        {
            UserId = Guid.Empty,
            TransactionId = deleteTransactionDto.TransactionId
        });
    }
}