namespace DeviceService.Contracts.Models.Response;

/// <summary>
/// Датчик
/// </summary>
public class GetSensorResponse
{
    /// <summary>
    /// Идентификатор датчика
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название датчика
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Единица измерения
    /// </summary>
    public required string MeasurementSymbol { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public required DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public required DateTime LastUpdate { get; set; }
}