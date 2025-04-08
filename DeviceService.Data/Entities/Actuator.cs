using CommonLib.EFCore.Interfaces;
using DeviceService.Contracts.Enums;

namespace DeviceService.Data.Entities;

/// <summary>
/// Актуатор
/// </summary>
public class Actuator : IEntity
{
    /// <summary>
    /// Идентификатор 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя актуатора
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Состояние
    /// </summary>
    public ActuatorState State { get; set; }
    
    /// <summary>
    /// Навигационное поле
    /// </summary>
    public Device Device { get; set; }
    
    /// <summary>
    /// Внешний ключ
    /// </summary>
    public Guid DeviceId { get; set; }
    
    /// <summary>
    /// Дата создания 
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime LastUpdate { get; set; }
}