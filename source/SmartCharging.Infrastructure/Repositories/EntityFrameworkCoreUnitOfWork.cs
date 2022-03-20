using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
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
            return _factory.GetDbContext().SaveChanges();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            Debug.WriteLine($"{exception.Message}");
        }
        catch (DbUpdateException exception)
        {
            Debug.WriteLine($"{exception.Message}");
        }
        catch (Exception exception)
        {
            Debug.WriteLine($"{exception.Message}");
        }

        return 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _factory.GetDbContext().SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException exception)
        {
            Debug.WriteLine($"{exception.Message}");
        }
        catch (DbUpdateException exception)
        {
            Debug.WriteLine($"{exception.Message}");
        }
        catch (Exception exception)
        {
            Debug.WriteLine($"{exception.Message}");
        }

        return 0;
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