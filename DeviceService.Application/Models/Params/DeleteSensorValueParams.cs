namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель удаления показания
/// </summary>
public class DeleteSensorValueParams
{
    /// <summary>
    /// Идентификатор показания
    /// </summary>
    public required Guid Id { get; set; }
}