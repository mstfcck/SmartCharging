using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Repositories;

namespace SmartCharging.Infrastructure.Repositories;

public class EntityFrameworkCoreRepository<TEntity> : IEntityFrameworkCoreRepository<TEntity>
    where TEntity : class, IEntityFramework
{
    private readonly IEntityFrameworkCoreContextFactory _dbContextFactory;

    public EntityFrameworkCoreRepository(IEntityFrameworkCoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public void Create(TEntity entity)
    {
        _dbContextFactory.GetDbContext().Set<TEntity>().Add(entity);
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _dbContextFactory.GetDbContext().Set<TEntity>().AddAsync(entity);
    }

    public TEntity Read(int key)
    {
        return _dbContextFactory.GetDbContext().Set<TEntity>().Find(key);
    }

    public async Task<TEntity> ReadAsync(int key)
    {
        return await _dbContextFactory.GetDbContext().Set<TEntity>().FindAsync(key);
    }

    public IQueryable<TEntity> Read()
    {
        return _dbContextFactory.GetDbContext().Set<TEntity>();
    }

    public void Update(TEntity entity, int key)
    {
        if (entity == null) return;
        var exist = _dbContextFactory.GetDbContext().Set<TEntity>().Find(key);
        if (exist != null)
        {
            _dbContextFactory.GetDbContext().Entry(exist).CurrentValues.SetValues(entity);
        }
    }

    public async Task UpdateAsync(TEntity entity, int key)
    {
        if (entity == null) return;
        var exist = await _dbContextFactory.GetDbContext().Set<TEntity>().FindAsync(key);
        if (exist != null)
        {
            _dbContextFactory.GetDbContext().Entry(exist).CurrentValues.SetValues(entity);
        }
    }

    public void Delete(TEntity entity)
    {
        _dbContextFactory.GetDbContext().Set<TEntity>().Remove(entity);
    }

    #region Aggreates

    public int Count()
    {
        return _dbContextFactory.GetDbContext().Set<TEntity>().Count();
    }

    public async Task<int> CountAsync()
    {
        return await _dbContextFactory.GetDbContext().Set<TEntity>().CountAsync();
    }

    #endregion
}