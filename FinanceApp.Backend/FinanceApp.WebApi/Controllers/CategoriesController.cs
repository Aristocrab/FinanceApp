using FinanceApp.Application.Categories.Commands.CreateCategory;
using FinanceApp.Application.Categories.Commands.DeleteCategory;
using FinanceApp.Application.Categories.Commands.UpdateCategory;
using FinanceApp.Application.Categories.Queries.GetAllCategories;
using FinanceApp.WebApi.Controllers.Shared;
using FinanceApp.WebApi.Models.Categories;
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
        return _mediator.Send(new GetAllCategoriesQuery
        {
            UserId = Guid.Empty // todo
        });
    }
    
    [HttpPost("new")]
    public Task<Guid> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        return _mediator.Send(new CreateCategoryCommand
        {
            UserId = Guid.Empty,
            Name = createCategoryDto.Name
        });
    }
    
    [HttpPut("update")]
    public Task<Guid> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        return _mediator.Send(new UpdateCategoryCommand
        {
            UserId = Guid.Empty,
            Name = updateCategoryDto.Name
        });
    }
    
    [HttpDelete("delete")]
    public Task DeleteCategory([FromBody] DeleteCategoryDto deleteCategoryDto)
    {
        return _mediator.Send(new DeleteCategoryCommand
        {
            UserId = Guid.Empty,
            CategoryId = deleteCategoryDto.CategoryId
        });
    }
}