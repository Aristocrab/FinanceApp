using MediatR;

namespace FinanceApp.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid AccountId { get; set; }
    
    public string Name { get; set; } = null!;
}