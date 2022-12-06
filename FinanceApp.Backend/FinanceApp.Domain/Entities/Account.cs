using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Balance { get; set; }
    public required Currency Currency { get; set; }

    public List<Transaction> Transactions { get; set; } = null!;
}