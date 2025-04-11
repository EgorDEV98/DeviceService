namespace DeviceService.Data.Entities;

/// <summary>
/// Показание с датчика
/// </summary>
public class SensorValue
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Показание
    /// </summary>
    public float Value { get; set; }
    
    /// <summary>
    /// Дата измерения
    /// </summary>
    public DateTime MeasurementDate { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public Sensor? Sensor { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public Guid SensorId { get; set; }
}
