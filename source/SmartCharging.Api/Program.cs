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

// // Sqlite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=SmartCharging.sqlite", optionsBuilder =>
    {
        optionsBuilder.MigrationsAssembly("SmartCharging.Infrastructure");
    }));

// // InMemoryDatabase:
// builder.Services.AddDbContext<ApplicationDbContext>(options => 
//     options.UseInMemoryDatabase("SmartCharging"));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.AddLogger();

builder.Services.AddSwagger();

try
{
    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    if (!serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.IsInMemory())
    {
        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()?.Database.EnsureCreated();
    }
    
    // Configure the HTTP request pipeline.
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