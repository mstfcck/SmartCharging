using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Exceptions;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Infrastructure.Repositories;

public class EntityFrameworkCoreUnitOfWork : IEntityFrameworkCoreUnitOfWork
{
    #region Constructor

    private readonly IEntityFrameworkCoreContextFactory _factory;

    public EntityFrameworkCoreUnitOfWork(IEntityFrameworkCoreContextFactory factory)
    {
        _factory = factory;
    }

    #endregion

    public IEntityFrameworkCoreRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntityFramework
    {
        return new EntityFrameworkCoreRepository<TEntity>(_factory);
    }

    public void BeginTransaction()
    {
        if (!_factory.GetDbContext().Database.IsInMemory())
        {
            _factory.GetDbContext().Database.BeginTransaction();
        }
    }

    public async Task BeginTransactionAsync()
    {
        if (!_factory.GetDbContext().Database.IsInMemory())
        {
            await _factory.GetDbContext().Database.BeginTransactionAsync();
        }
    }

    public int SaveChanges()
    {
        try
        {
            foreach (var entity in _factory.GetDbContext().ChangeTracker.Entries()
                         .Where(x => x.State is EntityState.Added or EntityState.Modified && x.Entity is IHasConcurrencyToken)
                         .Select(x => x.Entity as IHasConcurrencyToken))
            {
                entity.RowVersion = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
            
            return _factory.GetDbContext().SaveChanges();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new DatabaseException("DbUpdate Concurrency Exception", exception);
        }
        catch (DbUpdateException exception)
        {
            throw new DatabaseException("DbUpdate Exception", exception);
        }
        catch (Exception exception)
        {
            throw new DatabaseException("Db Exception", exception);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            foreach (var entity in _factory.GetDbContext().ChangeTracker.Entries()
                         .Where(x => x.State is EntityState.Added or EntityState.Modified && x.Entity is IHasConcurrencyToken)
                         .Select(x => x.Entity as IHasConcurrencyToken))
            {
                entity.RowVersion = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            return await _factory.GetDbContext().SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new DatabaseException("DbUpdate Concurrency Exception", exception);
        }
        catch (DbUpdateException exception)
        {
            throw new DatabaseException("DbUpdate Exception", exception);
        }
        catch (Exception exception)
        {
            throw new DatabaseException("Db Exception", exception);
        }
    }

    public void Commit()
    {
        if (!_factory.GetDbContext().Database.IsInMemory())
        {
            _factory.GetDbContext().Database.CommitTransaction();
        }
    }

    public async Task CommitAsync()
    {
        if (!_factory.GetDbContext().Database.IsInMemory())
        {
            await _factory.GetDbContext().Database.CommitTransactionAsync();
        }
    }

    public void Rollback()
    {
        if (!_factory.GetDbContext().Database.IsInMemory())
        {
            _factory.GetDbContext().Database.RollbackTransaction();
        }
    }

    public async Task RollbackAsync()
    {
        if (!_factory.GetDbContext().Database.IsInMemory())
        {
            await _factory.GetDbContext().Database.RollbackTransactionAsync();
        }
    }

    #region Dispose

    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _factory.GetDbContext().Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}