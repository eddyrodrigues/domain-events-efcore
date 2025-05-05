
namespace DomainEvents.EfCore;

public interface IUnitOfWork<T> where T : IAggregator
{
    
    void BeginTransaction();
    void Commit();
    void Rollback();

}
