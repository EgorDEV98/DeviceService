namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель получения списка датчиков
/// </summary>
public class GetSensorsParams
{
    /// <summary>
    /// Список идентификаторов датчиков
    /// </summary>
    public Guid[]? Ids { get; set; } 
    
    /// <summary>
    /// Список идентификаторов устройств
    /// </summary>
    public Guid[]? DeviceIds { get; set; }
    
    /// <summary>
    /// Список идентификаторов пользователей
    /// </summary>
    public Guid[]? UserIds { get; set; }
    
    /// <summary>
    /// Дата создания датчика С
    /// </summary>
    public DateTime? CreatedDateFrom { get; set; }
    
    /// <summary>
    /// Дата создания датчика До
    /// </summary>
    public DateTime? CreatedDateTo { get; set; }
    
    /// <summary>
    /// Дата редактирования датчика С
    /// </summary>
    public DateTime? LastUpdateFrom { get; set; }
    
    /// <summary>
    /// Дата редактирования датчика До
    /// </summary>
    public DateTime? LastUpdateTo{ get; set; }
    
    /// <summary>
    /// Отступ
    /// </summary>
    public int? Offset { get; set; }
    
    /// <summary>
    /// Кол-во
    /// </summary>
    public int? Limit { get; set; }
}