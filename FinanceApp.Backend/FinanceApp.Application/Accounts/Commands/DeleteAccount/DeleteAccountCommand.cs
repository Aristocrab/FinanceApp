using MediatR;

namespace FinanceApp.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommand : IRequest<Unit>
{
    public required Guid UserId { get; set; }
    public required Guid AccountId { get; set; }
}