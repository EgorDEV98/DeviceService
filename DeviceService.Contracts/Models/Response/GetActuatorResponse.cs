using DeviceService.Contracts.Enums;

namespace DeviceService.Contracts.Models.Response;

/// <summary>
/// Модель актуатора
/// </summary>
public class GetActuatorResponse
{
    /// <summary>
    /// Идентификатор актуатора
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название актуатора
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Состояния
    /// </summary>
    public required ActuatorState State { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public required DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата обновления
    /// </summary>
    public required DateTime LastUpdate { get; set; }
}