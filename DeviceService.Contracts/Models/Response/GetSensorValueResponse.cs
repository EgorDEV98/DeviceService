namespace DeviceService.Contracts.Models.Response;

/// <summary>
/// Модель показания датчика
/// </summary>
public class GetSensorValueResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Показание
    /// </summary>
    public required float Value { get; set; }
    
    /// <summary>
    /// Дата измерения
    /// </summary>
    public required DateTime MeasurementDate { get; set; }
    
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid SensorId { get; set; }
}