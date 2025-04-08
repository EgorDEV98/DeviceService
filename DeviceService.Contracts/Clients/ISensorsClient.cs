using CommonLib.Response;
using DeviceService.Contracts.Models.Request;
using DeviceService.Contracts.Models.Response;
using Refit;

namespace DeviceService.Contracts.Clients;

public interface ISensorsClient
{
    /// <summary>
    /// Получить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Get("/Sensors/{id}")]
    public Task<BaseResponse<GetSensorResponse>> GetSensorAsync(Guid id, CancellationToken ct);

    /// <summary>
    /// Получить список датчиков
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Get("/Sensors")]
    public Task<BaseResponse<IReadOnlyCollection<GetSensorResponse>>> GetSensorsResponse(
        [Query(CollectionFormat.Multi)] GetSensorsRequest request, CancellationToken ct);

    /// <summary>
    /// Добавить датчик к устройству
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Post("/Sensors/{deviceId}")]
    public Task<BaseResponse<GetSensorResponse>> AddSensorAsync(Guid deviceId, [Body] AddSensorRequest request,
        CancellationToken ct);

    /// <summary>
    /// Обновить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Patch("/Sensors/{id}")]
    public Task<BaseResponse<GetSensorResponse>> UpdateSensorAsync(Guid id, [Body] UpdateSensorRequest request,
        CancellationToken ct);

    /// <summary>
    /// Удалить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Delete("/Sensors/{id}")]
    public Task<BaseResponse> DeleteSensorAsync(Guid id, CancellationToken ct);
}