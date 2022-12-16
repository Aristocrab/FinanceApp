using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommand : IRequest<Guid>
{
    public Guid AccountId { get; set; }
    
    public string Name { get; set; } = null!;
    public required decimal Balance { get; set; }
    public required Currency Currency { get; set; }
    public required int Icon { get; set; }
}