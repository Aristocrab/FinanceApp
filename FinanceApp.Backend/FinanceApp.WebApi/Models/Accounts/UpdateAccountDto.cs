namespace FinanceApp.WebApi.Models.Accounts;

public class UpdateAccountDto
{
    public Guid AccountId { get; set; }
    public string Name { get; set; } = null!;
}