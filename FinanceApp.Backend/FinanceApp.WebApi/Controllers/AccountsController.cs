using FinanceApp.Application.Accounts.Commands.CreateAccount;
using FinanceApp.Application.Accounts.Commands.DeleteAccount;
using FinanceApp.Application.Accounts.Commands.UpdateAccount;
using FinanceApp.Application.Accounts.Queries.GetAllAccounts;
using FinanceApp.Domain.Enums;
using FinanceApp.WebApi.Controllers.Shared;
using FinanceApp.WebApi.Models.Accounts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.WebApi.Controllers;

[Authorize]
public class AccountsController : BaseController
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public Task<UserAccountsDto> GetAll()
    {
        return _mediator.Send(new GetAllAccountsQuery
        {
            UserId = UserId
        });
    }
    
    [HttpPost("new")]
    public Task<Guid> CreateAccount([FromBody] CreateAccountDto createAccountDto)
    {
        if (DemoMode) return Task.FromResult(Guid.Empty);
        
        var command = createAccountDto.Adapt<CreateAccountCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
    
    [HttpPut("update")]
    public Task<Guid> UpdateAccount([FromBody] UpdateAccountDto updateAccountDto)
    {
        if (DemoMode) return Task.FromResult(Guid.Empty);
        
        var command = updateAccountDto.Adapt<UpdateAccountCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
    
    [HttpDelete("delete")]
    public Task DeleteAccount([FromBody] DeleteAccountDto deleteAccountDto)
    {
        if (DemoMode) return Task.FromResult(Guid.Empty);
        
        var command = deleteAccountDto.Adapt<DeleteAccountCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
}