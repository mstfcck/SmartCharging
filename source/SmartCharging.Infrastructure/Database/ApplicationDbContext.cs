using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Entities;

namespace SmartCharging.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(GetType()));
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Group> Groups { get; set; }
    public DbSet<ChargeStation> ChargeStations { get; set; }
    public DbSet<Connector> Connectors { get; set; }
}
