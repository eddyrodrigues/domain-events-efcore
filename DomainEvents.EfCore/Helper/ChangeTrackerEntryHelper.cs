using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DomainEvents.EfCore.Helper;

public static class ChangeTrackerEntryHelper
{
    public static IDomainEventEntity[] GetDomainEventEntities(this ChangeTracker changeTracker)
    {
        return changeTracker.Entries<IDomainEventEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();
    }
}
