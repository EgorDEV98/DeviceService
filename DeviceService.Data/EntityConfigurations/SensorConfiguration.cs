using DeviceService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceService.Data.EntityConfigurations;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(x => x.MeasurementSymbol)
            .IsRequired()
            .HasMaxLength(10);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.LastUpdate).IsRequired();

        builder.HasOne(x => x.Device)
            .WithMany(x => x.Sensors)
            .HasForeignKey(x => x.DeviceId);
    }
}