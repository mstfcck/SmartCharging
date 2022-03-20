using Microsoft.EntityFrameworkCore;

namespace SmartCharging.Domain.Repositories;

public interface IEntityFrameworkCoreContextFactory
{
    DbContext GetDbContext();
}