using CommonLib.Response;
using DeviceService.Contracts.Models.Request;
using DeviceService.Contracts.Models.Response;
using Refit;

namespace DeviceService.Contracts.Clients;

/// <summary>
/// Клиент управления устройством
/// </summary>
public interface IDeviceClient
{
    /// <summary>
    /// Получить конкретное устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Get("/Devices/{id}")]
    public Task<BaseResponse<GetDeviceResponse>> GetDeviceAsync(Guid id, CancellationToken ct);

    /// <summary>
    /// Получить список устройств
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Get("/Devices")]
    public Task<BaseResponse<IReadOnlyCollection<GetDeviceResponse>>> GetDevicesAsync([Query(CollectionFormat.Multi)] GetDevicesRequest request, CancellationToken ct);

    /// <summary>
    /// Добавить устройство
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Параметры устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Post("/Devices/{userId}")]
    public Task<BaseResponse<GetDeviceResponse>> AddDeviceAsync(Guid userId, [Body] AddDeviceRequest request, CancellationToken ct);

    /// <summary>
    /// Обновить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="request">Новые параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Patch("/Devices/{id}")]
    public Task<BaseResponse<GetDeviceResponse>> UpdateDeviceAsync(Guid id, [Body] UpdateDeviceRequest request, CancellationToken ct);

    /// <summary>
    /// Удалить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Delete("/Devices/{id}")]
    public Task<BaseResponse> DeleteDeviceAsync(Guid id, CancellationToken ct);
}