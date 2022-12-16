using FinanceApp.Domain.Entities.Base;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Account : Entity
{
    public required string Name { get; set; }
    public required decimal Balance { get; set; }
    public required Currency Currency { get; set; }
    public required int Icon { get; set; }

    public List<Transaction> Transactions { get; set; } = null!;
}