namespace DeviceService.Contracts.Models.Request;

/// <summary>
/// Добавить устройство
/// </summary>
public class AddDeviceRequest
{
    /// <summary>
    /// Название устройства
    /// </summary>
    public required string Name { get; set; }
}