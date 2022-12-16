using FinanceApp.Domain.Enums;

namespace FinanceApp.WebApi.Models.Accounts;

public class CreateAccountDto
{
    public string Name { get; set; } = null!;
    public required decimal Balance { get; set; }
    public required Currency Currency { get; set; }
    public required int Icon { get; set; }
}