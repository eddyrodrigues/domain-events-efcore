using System;

namespace DomainEvents.EfCore;

public interface IDomainEventHandler<DomainEntity> where DomainEntity : IDomainEvent
{
    public Task HandleAsync(DomainEntity domainEvent, CancellationToken cancellationToken = default);
}
