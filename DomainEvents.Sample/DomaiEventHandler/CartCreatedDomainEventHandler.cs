using System;
using DomainEvents.EfCore;
using DomainEvents.Sample.DomainEvents;

namespace DomainEvents.Sample.DomaiEventHandler;

public class CartCreatedDomainEventHandler : IDomainEventHandler<CartCreatedDomainEvent>
{
    public Task HandleAsync(CartCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Handling the {nameof(CartCreatedDomainEvent)} with the CartId of {domainEvent.CartId}");
        return Task.CompletedTask;
    }
}
