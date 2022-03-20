namespace SmartCharging.Domain.Repositories;

public interface IEntityFrameworkCoreRepository<TEntity> where TEntity : class, IEntityFramework
{
    void Create(TEntity entity);
    Task CreateAsync(TEntity entity);

    TEntity Read(int key);
    Task<TEntity> ReadAsync(int key);

    IQueryable<TEntity> Read();

    void Update(TEntity entity, int key);
    Task UpdateAsync(TEntity entity, int key);

    void Delete(TEntity entity);

    #region Aggreates

    int Count();
    Task<int> CountAsync();

    #endregion
}