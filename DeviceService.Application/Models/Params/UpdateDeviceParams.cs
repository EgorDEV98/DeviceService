namespace DeviceService.Application.Models.Params;

/// <summary>
/// Обновление устройства
/// </summary>
public class UpdateDeviceParams
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Новое имя устройства
    /// </summary>
    public string? Name { get; set; }
}