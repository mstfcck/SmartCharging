using Microsoft.Extensions.DependencyInjection;

namespace SmartCharging.Infrastructure;

public static class Bootstrapper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}