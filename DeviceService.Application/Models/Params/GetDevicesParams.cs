namespace DeviceService.Application.Models.Params;

/// <summary>
/// Получить список устройств
/// </summary>
public class GetDevicesParams
{
    /// <summary>
    /// Список идентификаторов устройств
    /// </summary>
    public Guid[]? Ids { get; set; } 
    
    /// <summary>
    /// Список идентификаторов пользователей
    /// </summary>
    public Guid[]? UserIds { get; set; }
    
    /// <summary>
    /// Дата создания устройства С
    /// </summary>
    public DateTime? CreatedDateFrom { get; set; }
    
    /// <summary>
    /// Дата создания устройства До
    /// </summary>
    public DateTime? CreatedDateTo { get; set; }
    
    /// <summary>
    /// Дата редактирования устройства С
    /// </summary>
    public DateTime? LastUpdateFrom { get; set; }
    
    /// <summary>
    /// Дата редактирования устройства До
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