namespace FinanceApp.Domain.Exceptions;

public class UserExistsException : Exception
{
    public UserExistsException(string username) : base($"User with username '{username}' already exists.")
    {
        
    }
}