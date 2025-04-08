namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель удаления актуатора
/// </summary>
public class DeleteActuatorParams
{
    /// <summary>
    /// Идентификатор актуатора
    /// </summary>
    public required Guid Id { get; set; }
}