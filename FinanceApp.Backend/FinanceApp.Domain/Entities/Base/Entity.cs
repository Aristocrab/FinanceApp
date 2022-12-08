namespace FinanceApp.Domain.Entities.Base;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime TimeCreated { get; set; } = DateTime.Now;
}