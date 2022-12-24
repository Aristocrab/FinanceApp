using FinanceApp.Domain.Entities;
using MediatR;

namespace FinanceApp.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<User>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}