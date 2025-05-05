using System;
using DomainEvents.EfCore;

namespace DomainEvents.Sample.Entities;

public class CartEntity : BEntity
{
    public int Id { get; set; }
}
