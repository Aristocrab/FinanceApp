using FinanceApp.Domain.Enums;
using MediatR;

namespace FinanceApp.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest<Guid>
{
    public required Guid UserId { get; set; }
    
    public required string Name { get; set; }
    public required decimal Balance { get; set; }
    public required Currency Currency { get; set; }
    public required int Icon { get; set; }
}