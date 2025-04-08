using CommonLib.Response;
using DeviceService.Contracts.Models.Request;
using DeviceService.Contracts.Models.Response;
using Refit;

namespace DeviceService.Contracts.Clients;

public interface IActuatorsClient
{
    /// <summary>
    /// Получить конкретный актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Get("/Actuators/{id}")]
    public Task<BaseResponse<GetActuatorResponse>> GetActuatorAsync(Guid id, CancellationToken ct);

    /// <summary>
    /// Получить список актуаторов
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Get("/Actuators")]
    public Task<BaseResponse<IReadOnlyCollection<GetActuatorResponse>>> GetActuatorsAsync([Query(CollectionFormat.Multi)] GetActuatorsRequest request,
        CancellationToken ct);

    /// <summary>
    /// Добавить актуатор к устройству
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Post("/Actuators/{deviceId}")]
    public Task<BaseResponse<GetActuatorResponse>> AddActuatorAsync(Guid deviceId, [Body] AddActuatorRequest request,
        CancellationToken ct);

    /// <summary>
    /// Обновить актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="request">Новые параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Patch("/Actuators/{id}")]
    public Task<BaseResponse<GetActuatorResponse>> UpdateActuatorAsync(Guid id, [Body] UpdateActuatorRequest request,
        CancellationToken ct);

    /// <summary>
    /// Удалить актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Delete("/Actuators/{id}")]
    public Task<BaseResponse> DeleteActuatorAsync(Guid id, CancellationToken ct);

}