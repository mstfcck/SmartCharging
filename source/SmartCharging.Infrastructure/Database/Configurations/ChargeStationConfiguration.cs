using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCharging.Domain.Entities;

namespace SmartCharging.Infrastructure.Database.Configurations;

public class ChargeStationConfiguration : IEntityTypeConfiguration<ChargeStation>
{
    public void Configure(EntityTypeBuilder<ChargeStation> builder)
    {
        builder.ToTable(nameof(ChargeStation));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(x => x.Name).HasColumnName("Name");
        
        builder.Property(x => x.RowVersion)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();
        
        builder
            .HasMany(x => x.Connectors)
            .WithOne(x => x.ChargeStation)
            .HasForeignKey(x => x.ChargeStationId);
    }
}