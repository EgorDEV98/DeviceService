namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Модель обновления датчика
/// </summary>
public class UpdateSensorRequest
{
    /// <summary>
    /// Имя датчика
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public string? MeasurementSymbol { get; set; }
}