namespace DomainEvents.EfCore;

public interface IDomainEventEntity
{
    IEnumerable<IDomainEvent> DomainEvents { get; }
    void Raise(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
