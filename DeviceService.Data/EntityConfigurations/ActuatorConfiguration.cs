using CommonLib.Other.EnumConvertor;
using DeviceService.Contracts.Enums;
using DeviceService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeviceService.Data.EntityConfigurations;

public class ActuatorConfiguration : IEntityTypeConfiguration<Actuator>
{
    public void Configure(EntityTypeBuilder<Actuator> builder)
    {
        var stateConverter = new ValueConverter<ActuatorState, string>(
            x => EnumToString.ToEnumString(x),
            x => EnumToString.ToEnum<ActuatorState>(x),
            new ConverterMappingHints(size: 50, unicode: false));
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(x => x.State)
            .IsRequired()
            .HasDefaultValue(ActuatorState.Disable)
            .HasConversion(stateConverter);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.LastUpdate).IsRequired();
        
        builder.HasOne(x => x.Device)
            .WithMany(x => x.Actuators)
            .HasForeignKey(x => x.DeviceId);
    }
}