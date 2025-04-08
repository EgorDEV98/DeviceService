namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель добавления показания датчика
/// </summary>
public class AddSensorValueParams
{
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid SensorId { get; set; }
    
    /// <summary>
    /// Значение
    /// </summary>
    public required float Value { get; set; }
    
    /// <summary>
    /// Дата измерения
    /// </summary>
    public required DateTime MeasurementDate { get; set; }
}