using MediatR;

namespace FinanceApp.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
}