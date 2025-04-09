namespace DeviceService.Contracts.Models.Response;

public class GetDeviceResponse
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название устройства
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid UserId { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public required DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата обновления
    /// </summary>
    public required DateTime LastUpdate { get; set; }
    
    /// <summary>
    /// Датчики
    /// </summary>
    public IReadOnlyCollection<GetSensorResponse> Sensors { get; set; }
    
    /// <summary>
    /// Актуаторы
    /// </summary>
    public IReadOnlyCollection<GetActuatorResponse> Actuators { get; set; }
}