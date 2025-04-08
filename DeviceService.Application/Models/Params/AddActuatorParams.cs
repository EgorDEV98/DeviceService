namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель добавления актуатора
/// </summary>
public class AddActuatorParams
{
    /// <summary>
    /// Идентификатор устройства
    /// к которому добавляется актуатор
    /// </summary>
    public required Guid DeviceId { get; set; }
    
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public required string Name { get; set; }
}