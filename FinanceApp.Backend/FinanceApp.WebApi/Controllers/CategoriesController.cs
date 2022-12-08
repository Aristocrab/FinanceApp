﻿using FinanceApp.Application.Categories.Commands.CreateCategory;
using FinanceApp.Application.Categories.Commands.DeleteCategory;
using FinanceApp.Application.Categories.Commands.UpdateCategory;
using FinanceApp.Application.Categories.Queries.GetAllCategories;
using FinanceApp.Application.Categories.Queries.GetCategoriesStats;
using FinanceApp.Domain.Enums;
using FinanceApp.WebApi.Controllers.Shared;
using FinanceApp.WebApi.Models.Categories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.WebApi.Controllers;

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
        return _mediator.Send(new GetAllCategoriesQuery());
    }
    
    [HttpGet("stats/{transactionType}")]
    public Task<List<CategoryStatsDto>> GetCategoryStats(TransactionType transactionType)
    {
        return _mediator.Send(new GetCategoriesStatsQuery
        {
            Type = transactionType
        });
    }
    
    [HttpGet("stats/{transactionType}/{accountId}")]
    public Task<List<CategoryStatsDto>> GetCategoryStats(TransactionType transactionType, Guid accountId)
    {
        return _mediator.Send(new GetCategoriesStatsQuery
        {
            Type = transactionType,
            AccountId = accountId
        });
    }
    
    [HttpPost("new")]
    public Task<Guid> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        return _mediator.Send(createCategoryDto.Adapt<CreateCategoryCommand>());
    }
    
    [HttpPut("update")]
    public Task<Guid> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        return _mediator.Send(updateCategoryDto.Adapt<UpdateCategoryCommand>());
    }
    
    [HttpDelete("delete")]
    public Task DeleteCategory([FromBody] DeleteCategoryDto deleteCategoryDto)
    {
        return _mediator.Send(deleteCategoryDto.Adapt<DeleteCategoryCommand>());
    }
}