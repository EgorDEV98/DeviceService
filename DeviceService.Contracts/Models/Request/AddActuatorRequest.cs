namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Модель добавления актуатора
/// </summary>
public class AddActuatorRequest
{
    /// <summary>
    /// Имя актуатора
    /// </summary>
    public required string Name { get; set; }
}