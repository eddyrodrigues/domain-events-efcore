using System;
using Microsoft.Extensions.DependencyInjection;

namespace DomainEvents.EfCore;

public class DomainEventDispather : IDomainEventDispather
{
    private IServiceProvider _serviceProvider;

    public DomainEventDispather(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public void Dispath(IDomainEvent domainEvent)
    {
        var handlers = _serviceProvider.GetServices<IDomainEventHandler<IDomainEvent>>();

        foreach (var handler in handlers)
        {
            handler.HandleAsync(domainEvent).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }

    public async Task DispathAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // var handlers = (IDomainEventHandler<IDomainEvent>[]) _serviceProvider.GetKeyedServices(typeof(IDomainEventHandler<IDomainEvent>),Constants.DomainEventHandler.ToString());
        
        var domainEventType = domainEvent.GetType();

        var d = typeof(IDomainEventHandler<>).MakeGenericType(domainEventType);


        var handlers =  _serviceProvider.GetServices(d);

        foreach (var handler in handlers)
        {
            var handleMethod = handler.GetType().GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync));
            handleMethod.Invoke(handler, new object[] {domainEvent, cancellationToken});
        }
    }
}
