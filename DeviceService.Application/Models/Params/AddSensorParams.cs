namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель добавления датчика
/// </summary>
public class AddSensorParams
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid DeviceId { get; set; }
    
    /// <summary>
    /// Имя датчика
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public required string MeasurementSymbol { get; set; }
}