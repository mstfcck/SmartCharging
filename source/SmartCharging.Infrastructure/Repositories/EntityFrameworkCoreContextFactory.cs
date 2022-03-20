using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Repositories;
using SmartCharging.Infrastructure.Database;

namespace SmartCharging.Infrastructure.Repositories;

public class EntityFrameworkCoreContextFactory : IEntityFrameworkCoreContextFactory
{
    private readonly ApplicationDbContext _dbContext;

    public EntityFrameworkCoreContextFactory(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbContext GetDbContext()
    {
        return _dbContext;
    }
}