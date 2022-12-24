using FinanceApp.Domain.Entities;
using MediatR;

namespace FinanceApp.Application.Users.Queries.LoginUser;

public class LoginUserCommand : IRequest<User>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}