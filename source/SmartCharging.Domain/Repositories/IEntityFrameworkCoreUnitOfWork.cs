namespace SmartCharging.Domain.Repositories;

public interface IEntityFrameworkCoreUnitOfWork : IDisposable
{
    IEntityFrameworkCoreRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntityFramework;
    void BeginTransaction();
    Task BeginTransactionAsync();
    int SaveChanges();
    Task<int> SaveChangesAsync();
    void Commit();
    Task CommitAsync();
    void Rollback();
    Task RollbackAsync();
}