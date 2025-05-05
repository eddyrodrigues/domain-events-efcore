
using DomainEvents.EfCore;
using DomainEvents.Sample.Context;
using DomainEvents.Sample.DomaiEventHandler;
using DomainEvents.Sample.DomainEvents;
using DomainEvents.Sample.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var s = new ServiceCollection
{
    new ServiceDescriptor(typeof(IDomainEventHandler<CartCreatedDomainEvent>),typeof(CartCreatedDomainEventHandler), ServiceLifetime.Scoped)
};

var serviceProvider = s.BuildServiceProvider();
var myDb = new ConsoleDbContext(new DbContextOptions<ConsoleDbContext>() {});
var eventDispatcher = new DomainEventDispather(serviceProvider);

var unitOfWork = new CustomUnitOfWork(myDb, eventDispatcher);

// Creation of Domain Event
var cart = new CartEntity();
cart.Id = 1;
cart.Raise(new CartCreatedDomainEvent(1));

// Execution of handlers
unitOfWork.Context.Carts.Add(cart);
await unitOfWork.SaveChangesAsync();
