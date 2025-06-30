### What is it ?

This is a Domain Events Approach Solution read to plug in and use along EntityFramework Core (EfCore).

### Inspiration


[Domain events: Design and implementation](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation)

### How to use it ?

You can se in the test samples to see more in depth (DomainEvents.EfCore.Tests (Test Project) or DomainEvents.Sample). The general idea is to provide a set of classes, methods and interfaces to apply the domain events in a simple way.

#### usage:

Create a Entity with the abstract class `BEntity` (holds the list of raised domain events and methods to raise them)

Example:

```c#
public class Cart : BEntity
{
    public int Id { get; set; }
}
```

Raise a domain event:
```c#
var cartInstance = new Cart();
cartInstance.Id = 1;
cartInstance.Raise(new CartCreated(cartInstance.Id));
```

How does domain event looks like?

We need to use the IDomainEvent interface to ensure the internal implementation is going to find the defined domains.
```c#
public class CartCreatedDomainEvent : IDomainEvent
{
    public int CartId { get; set;}
    public CartCreatedDomainEvent(int cartId)
    {
        CartId = cartId;
    }
}

```


Then, we need to set our domain event handler, to ensure proper event handling.
It can be done like this, implementing the `IDomainEventHandler` interface:

```c#
public class CartCreatedDomainEventHandler : IDomainEventHandler<CartCreatedDomainEvent>
{
    public Task HandleAsync(CartCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Handling the {nameof(CartCreatedDomainEvent)} with the CartId of {domainEvent.CartId}");
        return Task.CompletedTask;
    }
}
```


Create you Unit of Work approach to hold the helper methods:

```c#
var uow = new UnitOfWorkSystemContext(dbContext, domainEventsDispather);
```


Adds Cart to the Database:

```c#

// Instance of UoW

uow.DbContext.Carts.Add(cartInstance);
await uow.SaveChangesAsync(CancellationToken.None);
```

Expected output:

```
Handling the CartCreatedDomainEvent with the CartId of 1
```