﻿using FinanceApp.Domain.Enums;

namespace FinanceApp.WebApi.Models.Accounts;

public class UpdateAccountDto
{
    public Guid AccountId { get; set; }
    
    public string Name { get; set; } = null!;
    public required decimal Balance { get; set; }
    public required Currency Currency { get; set; }
    public required int Icon { get; set; }
}