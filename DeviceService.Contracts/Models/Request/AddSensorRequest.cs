namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Модель добавления датчика
/// </summary>
public class AddSensorRequest
{
    /// <summary>
    /// Имя датчика
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public required string MeasurementSymbol { get; set; }
}