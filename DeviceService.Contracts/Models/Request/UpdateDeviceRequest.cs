namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Обновление устройства
/// </summary>
public class UpdateDeviceRequest
{
    /// <summary>
    /// Новое имя устройства
    /// </summary>
    public string? Name { get; set; }
}