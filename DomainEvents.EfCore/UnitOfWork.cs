using DomainEvents.EfCore.Helper;
namespace DomainEvents.EfCore;

public abstract class UnitOfWork<TDbContext> : IDisposable, IAsyncDisposable where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private TDbContext _dbContext;
    
    public TDbContext Context => _dbContext;
    
    private readonly IDomainEventDispather _domainEventDispather;
    private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction _transaction;

    public UnitOfWork(TDbContext dbContext, IDomainEventDispather domainEventDispather)
    {
        _dbContext = dbContext;
        this._domainEventDispather = domainEventDispather;
    }

    public async Task<DomainEventsProcessResult> ProcessDomainEventsAsync(IDomainEventEntity[] domainEventEntityps, CancellationToken cancellationToken = default)
    {
        var processResult = new DomainEventsProcessResult();
        foreach (var entity in domainEventEntityps)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents(); // Ensures to not call it again
            foreach (var domainEvent in events)
            {
                await _domainEventDispather.DispathAsync(domainEvent, cancellationToken);
                processResult.AddProcessedEvent(domainEvent);
            }
        }

        return processResult;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = _dbContext.ChangeTracker.GetDomainEventEntities();
        await ProcessDomainEventsAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void BeginTransaction()
    {
        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        _transaction?.Commit();
    }

    public void CommitAsync(CancellationToken cancellationToken = default)
    {
        _transaction?.CommitAsync(cancellationToken);
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }
    
    public void RollbackAsync(CancellationToken cancellationToken = default)
    {
        _transaction?.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext?.DisposeAsync() ?? ValueTask.CompletedTask;
    }
}
