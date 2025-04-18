namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель обновления датчика
/// </summary>
public class UpdateSensorParams
{
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Имя датчика
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public string? MeasurementSymbol { get; set; }
}