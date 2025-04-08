using DeviceService.Contracts.Enums;

namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Модель обновления актуатора
/// </summary>
public class UpdateActuatorRequest
{
    /// <summary>
    /// Состояние актуатора
    /// </summary>
    public ActuatorState? State { get; set; }
    
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public string? Name { get; set; }
}