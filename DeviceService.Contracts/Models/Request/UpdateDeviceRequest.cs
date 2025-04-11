namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Модель обновления устройства
/// </summary>
public class UpdateDeviceRequest
{
    /// <summary>
    /// Новое имя устройства
    /// </summary>
    public string? Name { get; set; }
}