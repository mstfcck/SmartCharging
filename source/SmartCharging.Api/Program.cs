using Microsoft.EntityFrameworkCore;
using Serilog;
using SmartCharging.Api.Extensions;
using SmartCharging.Application;
using SmartCharging.Core.Extensions;
using SmartCharging.Core.Middlewares;
using SmartCharging.Infrastructure;
using SmartCharging.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddOptions();
builder.Services.AddLogging();

builder.AddLogger();

builder.AddDatabaseProvider();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

try
{
    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    await using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();

    if (!serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.IsInMemory())
    {
        if ((await serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.EnsureCreatedAsync()))
        {
            await serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.MigrateAsync();
            Log.Information("Database is created");
        }
    }
    else
    {
        Log.Information("Database is working as InMemory");
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

    Log.Information("Application running");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
}
finally
{
    Log.CloseAndFlush();
}