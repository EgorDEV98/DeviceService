using CommonLib.EFCore.Interfaces;

namespace DeviceService.Data.Entities;

/// <summary>
/// Устройство
/// </summary>
public class Device : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название устройства
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя владельца устройства
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Список датчиков устройства
    /// </summary>
    public ICollection<Sensor> Sensors { get; set; }
    
    /// <summary>
    /// Список актуаторов устройства
    /// </summary>
    public ICollection<Actuator> Actuators { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime LastUpdate { get; set; }
}