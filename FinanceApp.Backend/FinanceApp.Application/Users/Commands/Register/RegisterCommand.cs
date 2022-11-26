using MediatR;

namespace FinanceApp.Application.Users.Commands.Register;

public class RegisterCommand : IRequest<Guid>
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}