using Microsoft.Extensions.DependencyInjection;
using SmartCharging.Domain.Repositories;
using SmartCharging.Infrastructure.Repositories;

namespace SmartCharging.Infrastructure;

public static class Bootstrapper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IEntityFrameworkCoreContextFactory, EntityFrameworkCoreContextFactory>();
        services.AddScoped<IEntityFrameworkCoreUnitOfWork, EntityFrameworkCoreUnitOfWork>();
        services.AddScoped(typeof(IEntityFrameworkCoreRepository<>), typeof(EntityFrameworkCoreRepository<>));
        return services;
    }
}