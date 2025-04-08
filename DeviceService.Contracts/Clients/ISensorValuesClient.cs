using CommonLib.Response;
using DeviceService.Contracts.Models.Request;
using DeviceService.Contracts.Models.Response;
using Refit;

namespace DeviceService.Contracts.Clients;

public interface ISensorValuesClient
{
    /// <summary>
    /// Получить показание датчика
    /// </summary>
    /// <param name="id">Идентификатор показания</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Get("/SensorValues/{id}")]
    public Task<BaseResponse<GetSensorValueResponse>> GetSensorValueAsync(Guid id, CancellationToken ct);

    /// <summary>
    /// Получить список показаний датчиков
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Get("/SensorValues")]
    public Task<BaseResponse<IReadOnlyCollection<GetSensorValueResponse>>> GetSensorValuesAsync(
        [Query(CollectionFormat.Multi)] GetSensorValuesRequest request, CancellationToken ct);

    /// <summary>
    /// Добавить показание к датчику
    /// </summary>
    /// <param name="sensorId"></param>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [Post("/SensorValues/{sensorId}")]
    public Task<BaseResponse<GetSensorValueResponse>> AddSensorValueAsync(Guid sensorId,
        [Body] AddSensorValueRequest request, CancellationToken ct);
    
    /// <summary>
    /// Удалить показание датчика
    /// </summary>
    /// <param name="id">Идентификатор показания</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Delete("/SensorValues/{id}")]
    public Task<BaseResponse> DeleteSensorValueAsync(Guid id, CancellationToken ct);

    /// <summary>
    /// Очистить показания датчика
    /// </summary>
    /// <param name="sensorId">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Delete("/SensorValues/{sensorId}/params")]
    public Task<BaseResponse> TruncateSensorValuesAsync(Guid sensorId, [Query(CollectionFormat.Multi)] DeleteSensorValuesRequest request, CancellationToken ct);
}