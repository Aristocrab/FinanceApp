﻿using MediatR;

namespace FinanceApp.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
}