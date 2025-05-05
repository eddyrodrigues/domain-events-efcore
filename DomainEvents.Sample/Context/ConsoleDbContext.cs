using System;
using DomainEvents.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainEvents.Sample.Context;

public class ConsoleDbContext : DbContext
{
    public DbSet<CartEntity> Carts {get; set;}
    public ConsoleDbContext(DbContextOptions<ConsoleDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("db");
    }
}
