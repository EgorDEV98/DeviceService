namespace DeviceService.Application.Models.Params;

/// <summary>
/// Добавить устройство
/// </summary>
public class AddDeviceParams
{
    /// <summary>
    /// Идентификатор пользователя к которому добавляется устройство
    /// </summary>
    public required Guid UserId { get; set; }
    
    /// <summary>
    /// Название устройства
    /// </summary>
    public required string Name { get; set; }
}