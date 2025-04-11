using DeviceService.Data.Entities;
using DeviceService.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Data;

public class DeviceServiceDbContext(DbContextOptions<DeviceServiceDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Устройства
    /// </summary>
    public DbSet<Device> Devices { get; set; }
    
    /// <summary>
    /// Актуаторы
    /// </summary>
    public DbSet<Actuator> Actuators { get; set; }
    
    /// <summary>
    /// Датчики
    /// </summary>
    public DbSet<Sensor> Sensors { get; set; }
    
    /// <summary>
    /// Показания датчиков
    /// </summary>
    public DbSet<SensorValue> SensorValues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        modelBuilder.ApplyConfiguration(new ActuatorConfiguration());
        modelBuilder.ApplyConfiguration(new SensorConfiguration());
        modelBuilder.ApplyConfiguration(new SensorValueConfiguration());
    }
}