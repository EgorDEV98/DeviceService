using DeviceService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeviceService.Data.EntityConfigurations;

public class SensorValueConfiguration : IEntityTypeConfiguration<SensorValue>
{
    public void Configure(EntityTypeBuilder<SensorValue> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Value).IsRequired();
        builder.Property(x => x.MeasurementDate).IsRequired();

        builder.HasOne(x => x.Sensor)
            .WithMany(x => x.SensorValues)
            .HasForeignKey(x => x.SensorId);
    }
}