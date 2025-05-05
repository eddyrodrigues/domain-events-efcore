using System;

namespace DomainEvents.EfCore;

public class DomainEventsProcessResult
{
    private ICollection<IDomainEvent> _processedEvents;

    public IDomainEvent[] ProcessedEvents => [.. _processedEvents];

    public DomainEventsProcessResult()
    {
        _processedEvents = [];
    }

    public void AddProcessedEvent(IDomainEvent domainEvent)
    {
        _processedEvents.Add(domainEvent);
    }
    
}
