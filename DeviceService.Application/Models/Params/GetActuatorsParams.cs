namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель получения списка актуаторов
/// </summary>
public class GetActuatorsParams
{
    /// <summary>
    /// Список идентификаторов актуаторов
    /// </summary>
    public Guid[]? Ids { get; set; } 
    
    /// <summary>
    /// Список идентификаторов пользователей
    /// </summary>
    public Guid[]? UserIds { get; set; }
    
    /// <summary>
    /// Дата создания актуатора С
    /// </summary>
    public DateTime? CreatedDateFrom { get; set; }
    
    /// <summary>
    /// Дата создания актуатора До
    /// </summary>
    public DateTime? CreatedDateTo { get; set; }
    
    /// <summary>
    /// Дата редактирования актуатора С
    /// </summary>
    public DateTime? LastUpdateFrom { get; set; }
    
    /// <summary>
    /// Дата редактирования актуатора До
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