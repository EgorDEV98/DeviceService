namespace DeviceService.Contracts.Models.Response;

/// <summary>
/// Модель получения показания датчика
/// </summary>
public class GetSensorValueResponse
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
    /// Идентификатор датчика
    /// </summary>
    public Guid SensorId { get; set; }
}