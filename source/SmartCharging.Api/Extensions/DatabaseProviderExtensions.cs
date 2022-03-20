using Microsoft.EntityFrameworkCore;
using SmartCharging.Infrastructure.Database;

namespace SmartCharging.Api.Extensions;

public static class DatabaseProviderExtensions
{
    public static void AddDatabaseProvider(this WebApplicationBuilder builder)
    {
        var provider = builder.Configuration.GetValue("DatabaseSettings:Provider", "InMemory");

        switch (provider)
        {
            case DatabaseProvider.Sqlite:
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(builder.Configuration["DatabaseSettings:SqliteConnection"],
                        optionsBuilder => { optionsBuilder.MigrationsAssembly("SmartCharging.Infrastructure"); }));
                break;
            case DatabaseProvider.InMemory:
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("SmartCharging"));
                break;
            default:
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("SmartCharging"));
                break;
        }
    }
}