using FinanceApp.Application.Categories.Commands.CreateCategory;
using FinanceApp.Application.Categories.Commands.DeleteCategory;
using FinanceApp.Application.Categories.Commands.UpdateCategory;
using FinanceApp.Application.Categories.Queries.GetAllCategories;
using FinanceApp.Application.Categories.Queries.GetCategoriesStats;
using FinanceApp.Domain.Enums;
using FinanceApp.WebApi.Controllers.Shared;
using FinanceApp.WebApi.Models.Categories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.WebApi.Controllers;

[Authorize]
public class CategoriesController : BaseController
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public Task<List<CategoryDto>> GetAll()
    {
        return _mediator.Send(new GetAllCategoriesQuery
        {
            UserId = UserId
        });
    }
    
    [HttpGet("stats/{transactionType}")]
    public Task<List<CategoryStatsDto>> GetCategoryStats(TransactionType transactionType)
    {
        return _mediator.Send(new GetCategoriesStatsQuery
        {
            UserId = UserId,
            Type = transactionType
        });
    }
    
    [HttpGet("stats/{transactionType}/{accountId}")]
    public Task<List<CategoryStatsDto>> GetCategoryStats(TransactionType transactionType, Guid accountId)
    {
        return _mediator.Send(new GetCategoriesStatsQuery
        {
            UserId = UserId,
            Type = transactionType,
            AccountId = accountId
        });
    }
    
    [HttpPost("new")]
    public Task<Guid> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        var command = createCategoryDto.Adapt<CreateCategoryCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
    
    [HttpPut("update")]
    public Task<Guid> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        var command = updateCategoryDto.Adapt<UpdateCategoryCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
    
    [HttpDelete("delete")]
    public Task DeleteCategory([FromBody] DeleteCategoryDto deleteCategoryDto)
    {
        var command = deleteCategoryDto.Adapt<DeleteCategoryCommand>();
        command.UserId = UserId;
        return _mediator.Send(command);
    }
}