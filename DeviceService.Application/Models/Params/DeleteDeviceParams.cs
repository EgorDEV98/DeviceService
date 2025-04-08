namespace DeviceService.Application.Models.Params;

/// <summary>
/// Удаление устройства
/// </summary>
public class DeleteDeviceParams
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid Id { get; set; }
}