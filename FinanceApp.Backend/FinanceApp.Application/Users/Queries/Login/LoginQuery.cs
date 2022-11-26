using MediatR;

namespace FinanceApp.Application.Users.Queries.Login;

public class LoginQuery : IRequest<Guid>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}