using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCharging.Domain.Entities;

namespace SmartCharging.Infrastructure.Database.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(nameof(Group));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.CapacityInAmps).HasColumnName("CapacityInAmps");
        
        builder.Property(x => x.RowVersion)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();
        
        builder
            .HasMany(x => x.ChargeStations)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId);
    }
}