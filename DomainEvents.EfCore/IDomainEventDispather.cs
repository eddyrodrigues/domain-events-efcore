
namespace DomainEvents.EfCore;

public interface IDomainEventDispather
{
    void Dispath(IDomainEvent domainEvent);
    Task DispathAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
