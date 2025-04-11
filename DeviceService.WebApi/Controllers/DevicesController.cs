using CommonLib.Response;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Mappers;
using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Clients;
using DeviceService.Contracts.Models.Request;
using DeviceService.Contracts.Models.Response;
using DeviceService.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DevicesController : ControllerBase, IDevicesClient
{
    private readonly IDevicesService _devicesService;
    private readonly DeviceControllerMapper _mapper;

    public DevicesController(IDevicesService devicesService, DeviceControllerMapper mapper)
    {
        _devicesService = devicesService;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<BaseResponse<GetDeviceResponse>> GetDeviceAsync([FromRoute] Guid id, CancellationToken ct)
        => AppResponse.Create(await _devicesService.GetDeviceAsync(new GetDeviceParams()
        {
            Id = id
        }, ct));

    /// <summary>
    /// Получить список устройств
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<IReadOnlyCollection<GetDeviceResponse>>> GetDevicesAsync([FromQuery] GetDevicesRequest request, CancellationToken ct)
        => AppResponse.Create(await _devicesService.GetDevicesAsync(_mapper.Map(request), ct));

    /// <summary>
    /// Добавить устройство пользователю
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Параметры устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public async Task<BaseResponse<GetDeviceResponse>> AddDeviceAsync([FromRoute] Guid userId, [FromBody] AddDeviceRequest request, CancellationToken ct)
        => AppResponse.Create(await _devicesService.AddDeviceAsync(new AddDeviceParams()
        {
            UserId = userId,
            Name = request.Name
        }, ct));

    /// <summary>
    /// Обновить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="request">Новые параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<BaseResponse<GetDeviceResponse>> UpdateDeviceAsync([FromRoute] Guid id,[FromBody] UpdateDeviceRequest request, CancellationToken ct)
        => AppResponse.Create(await _devicesService.UpdateDeviceAsync(new UpdateDeviceParams()
        {
            Id = id,
            Name = request.Name
        }, ct));

    /// <summary>
    /// Удалить устройство
    /// </summary>
    /// <param name="id">Идентификатор устройства</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<BaseResponse> DeleteDeviceAsync([FromRoute] Guid id, CancellationToken ct)
    {
        await _devicesService.DeleteDeviceAsync(new DeleteDeviceParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create();
    }
}