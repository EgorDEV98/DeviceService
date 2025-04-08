using CommonLib.Response;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Clients;
using DeviceService.Contracts.Models.Request;
using DeviceService.Contracts.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DevicesController : ControllerBase, IDeviceClient
{
    private readonly IDeviceService _deviceService;

    public DevicesController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    /// <summary>
    /// Получить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<BaseResponse<GetDeviceResponse>> GetDeviceAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await _deviceService.GetDeviceAsync(new GetDeviceParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Получить список устройств
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<IReadOnlyCollection<GetDeviceResponse>>> GetDevicesAsync([FromQuery] GetDevicesRequest request, CancellationToken ct)
    {
        var result = await _deviceService.GetDevicesAsync(new GetDevicesParams()
        {
            Ids = request.Ids,
            UserIds = request.UserIds,
            CreatedDateFrom = request.CreatedDateFrom,
            CreatedDateTo = request.CreatedDateTo,
            LastUpdateFrom = request.LastUpdateFrom,
            LastUpdateTo = request.LastUpdateTo,
            Offset = request.Offset,
            Limit = request.Limit
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Добавить устройство пользователю
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Параметры устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public async Task<BaseResponse<GetDeviceResponse>> AddDeviceAsync([FromRoute] Guid userId, [FromBody] AddDeviceRequest request, CancellationToken ct)
    {
        var result = await _deviceService.AddDeviceAsync(new AddDeviceParams()
        {
            UserId = userId,
            Name = request.Name
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Обновить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="request">Новые параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<BaseResponse<GetDeviceResponse>> UpdateDeviceAsync([FromRoute] Guid id,[FromBody] UpdateDeviceRequest request, CancellationToken ct)
    {
        var result = await _deviceService.UpdateDeviceAsync(new UpdateDeviceParams()
        {
            Id = id,
            Name = request.Name
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Удалить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<BaseResponse> DeleteDeviceAsync([FromRoute] Guid id, CancellationToken ct)
    {
        await _deviceService.DeleteDeviceAsync(new DeleteDeviceParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create();
    }
}