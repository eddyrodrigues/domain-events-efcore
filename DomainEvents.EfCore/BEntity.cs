namespace DomainEvents.EfCore;

public abstract class BEntity : IDomainEventEntity
{
    private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
