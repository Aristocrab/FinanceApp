﻿using FinanceApp.Application.Accounts.Commands.CreateAccount;
using FinanceApp.Application.Accounts.Commands.DeleteAccount;
using FinanceApp.Application.Accounts.Commands.UpdateAccount;
using FinanceApp.Application.Accounts.Queries.GetAllAccounts;
using FinanceApp.WebApi.Controllers.Shared;
using FinanceApp.WebApi.Models.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.WebApi.Controllers;

public class AccountsController : BaseController
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public Task<List<AccountDto>> GetAll()
    {
        return _mediator.Send(new GetAllAccountsQuery
        {
        });
    }
    
    [HttpPost("new")]
    public Task<Guid> CreateAccount([FromBody] CreateAccountDto createAccountDto)
    {
        return _mediator.Send(new CreateAccountCommand
        {
            Name = createAccountDto.Name
        });
    }
    
    [HttpPut("update")]
    public Task<Guid> UpdateAccount([FromBody] UpdateAccountDto updateAccountDto)
    {
        return _mediator.Send(new UpdateAccountCommand
        {
            Name = updateAccountDto.Name,
            AccountId = updateAccountDto.AccountId
        });
    }
    
    [HttpDelete("delete")]
    public Task DeleteAccount([FromBody] DeleteAccountDto deleteAccountDto)
    {
        return _mediator.Send(new DeleteAccountCommand
        {
            AccountId = deleteAccountDto.AccountId
        });
    }
}