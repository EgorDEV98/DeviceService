namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель удаления устройства
/// </summary>
public class DeleteDeviceParams
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid Id { get; set; }
}