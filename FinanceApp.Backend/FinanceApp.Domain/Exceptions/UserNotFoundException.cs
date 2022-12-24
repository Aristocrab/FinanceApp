namespace FinanceApp.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string username)
    : base($"User with Username '{username}' was not found") { }
    
    public UserNotFoundException()
    : base($"User was not found") { }
}