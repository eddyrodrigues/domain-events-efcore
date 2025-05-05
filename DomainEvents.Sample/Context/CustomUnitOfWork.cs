using System;
using DomainEvents.EfCore;

namespace DomainEvents.Sample.Context;

public class CustomUnitOfWork : UnitOfWork<ConsoleDbContext>
{
    public CustomUnitOfWork(ConsoleDbContext dbContext, IDomainEventDispather domainEventDispather) : base(dbContext, domainEventDispather)
    {
    }
}
