using Microsoft.EntityFrameworkCore;
using Moq;

namespace DomainEvents.EfCore.Tests;

public class NewDomainEvent(int cancelCartId) : IDomainEvent
{
}

public class MyEntity : BEntity
{
    public int Id { get; }

    public MyEntity(int id)
    {
        Id = id;
    }
}

public class UnitOfWorkSystemContext : UnitOfWork<DbContext>
{
    public UnitOfWorkSystemContext(DbContext dbContext, IDomainEventDispather domainEventDispather) : base(dbContext, domainEventDispather)
    {
    }
}

public class UnitTest1
{
    [Fact]
    public async Task When_DomainEventsExists_Dispatch()
    {

        var dbContextMock = new Mock<DbContext>();
        var domainEventsDispatherMock = new Mock<IDomainEventDispather>();
        var entity = new MyEntity(2);
        var entity2 = new MyEntity(1);
        entity.Raise(new NewDomainEvent(1));

        var uow = new UnitOfWorkSystemContext(dbContextMock.Object, domainEventsDispatherMock.Object);

        MyEntity[] entities = [entity, entity2]; 

        var processResult = await uow.ProcessDomainEventsAsync(entities, CancellationToken.None);
        Assert.Single(processResult.ProcessedEvents);
    }
}
