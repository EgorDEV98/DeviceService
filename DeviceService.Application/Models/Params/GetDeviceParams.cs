namespace DeviceService.Application.Models.Params;

/// <summary>
/// Получить устройство
/// </summary>
public class GetDeviceParams
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid Id { get; set; }
}