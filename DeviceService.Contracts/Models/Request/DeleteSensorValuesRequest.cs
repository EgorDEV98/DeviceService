namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Модель массового удаления показателей
/// </summary>
public class DeleteSensorValuesRequest
{
    /// <summary>
    /// Дата измерений С
    /// </summary>
    public DateTime? MeasurementDateFrom { get; set; }
    
    /// <summary>
    /// Дата измерений До
    /// </summary>
    public DateTime? MeasurementDateTo { get; set; }
}