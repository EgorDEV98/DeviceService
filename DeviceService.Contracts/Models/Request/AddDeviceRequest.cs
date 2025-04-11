namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Модель добавления устройства
/// </summary>
public class AddDeviceRequest
{
    /// <summary>
    /// Название устройства
    /// </summary>
    public required string Name { get; set; }
}