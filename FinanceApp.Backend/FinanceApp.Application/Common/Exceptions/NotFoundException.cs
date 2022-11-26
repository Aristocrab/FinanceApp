namespace FinanceApp.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entity, Guid id) 
        : base($"Enitity {entity} with id '{id}' was not found.") { }
}