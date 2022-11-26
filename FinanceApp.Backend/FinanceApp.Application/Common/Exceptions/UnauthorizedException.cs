using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(User user, string entity, Guid enitiyId)
        : base($"User {user.Id} does not own enitiy {entity} with id {enitiyId}") { }
}