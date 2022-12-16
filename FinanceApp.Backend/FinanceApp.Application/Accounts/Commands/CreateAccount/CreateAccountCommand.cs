using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public required decimal Balance { get; set; }
    public required Currency Currency { get; set; }
    public required int Icon { get; set; }
}