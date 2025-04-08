namespace DeviceService.Application.Models.Params;

/// <summary>
/// Модель получения показаний датчика
/// </summary>
public class GetSensorValueParams
{
    /// <summary>
    /// Идентификатор показания
    /// </summary>
    public required Guid Id { get; set; }
}