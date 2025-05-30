namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель получения списка показаний
/// </summary>
public class GetSensorValuesParams
{
    /// <summary>
    /// Список идентификаторов показаний
    /// </summary>
    public Guid[]? Ids { get; set; }
    
    /// <summary>
    /// Список идентификаторов датчиков
    /// </summary>
    public Guid[]? SensorIds { get; set; }
    
    /// <summary>
    /// Дата измерений С
    /// </summary>
    public DateTime? MeasurementDateFrom { get; set; }
    
    /// <summary>
    /// Дата измерений До
    /// </summary>
    public DateTime? MeasurementDateTo { get; set; }
    
    /// <summary>
    /// Отступ
    /// </summary>
    public int? Offset { get; set; }
    
    /// <summary>
    /// Лимит
    /// </summary>
    public int? Limit { get; set; }
}