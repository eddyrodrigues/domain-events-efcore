using System;
using DomainEvents.EfCore;

namespace DomainEvents.Sample.DomainEvents;

public class CartCreatedDomainEvent : IDomainEvent
{
    public int CartId { get; set;}
    public CartCreatedDomainEvent(int cartId)
    {
        CartId = cartId;
    }
}
