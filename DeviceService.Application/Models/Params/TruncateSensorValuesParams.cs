namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель удаления всех показателей с датчика
/// </summary>
public class TruncateSensorValuesParams
{
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid SensorId { get; set; }
    
    /// <summary>
    /// Дата измерений С
    /// </summary>
    public DateTime? MeasurementDateFrom { get; set; }
    
    /// <summary>
    /// Дата измерений До
    /// </summary>
    public DateTime? MeasurementDateTo { get; set; }
}