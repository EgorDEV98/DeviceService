using CommonLib.EFCore.Interfaces;

namespace DeviceService.Data.Entities;

/// <summary>
/// Датчик
/// </summary>
public class Sensor : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Единица измерений
    /// </summary>
    public required string MeasurementSymbol { get; set; }

    /// <summary>
    /// Навигационное поле
    /// </summary>
    public Device? Device { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public Guid DeviceId { get; set; }
    
    /// <summary>
    /// Результаты измерений
    /// </summary>
    public ICollection<SensorValue> SensorValues { get; set; } 
        = new List<SensorValue>();
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime LastUpdate { get; set; }
}