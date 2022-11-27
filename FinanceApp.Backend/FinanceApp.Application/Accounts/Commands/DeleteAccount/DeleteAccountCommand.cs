using MediatR;

namespace FinanceApp.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommand : IRequest<Unit>
{
    public Guid AccountId { get; set; }
}