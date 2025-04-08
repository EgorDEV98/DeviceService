namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Модель добавления показания датчика
/// </summary>
public class AddSensorValueRequest
{
    /// <summary>
    /// Значение
    /// </summary>
    public required float Value { get; set; }
    
    /// <summary>
    /// Дата измерения
    /// </summary>
    public required DateTime MeasurementDate { get; set; }
}