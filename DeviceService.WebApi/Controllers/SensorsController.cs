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
public class SensorsController : ControllerBase, ISensorsClient
{
    private readonly ISensorsService _sensorsService;

    public SensorsController(ISensorsService sensorsService)
    {
        _sensorsService = sensorsService;
    }
    
    /// <summary>
    /// Получить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<BaseResponse<GetSensorResponse>> GetSensorAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await _sensorsService.GetSensorAsync(new GetSensorParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Получить список датчиков
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<IReadOnlyCollection<GetSensorResponse>>> GetSensorsResponse([FromQuery] GetSensorsRequest request, CancellationToken ct)
    {
        var result = await _sensorsService.GetSensorsAsync(new GetSensorsParams()
        {
            Ids = request.Ids,
            UserIds = request.UserIds,
            DeviceIds = request.DeviceIds,
            CreatedDateFrom = request.CreatedDateFrom,
            CreatedDateTo = request.CreatedDateTo,
            LastUpdateFrom = request.LastUpdateFrom,
            LastUpdateTo = request.CreatedDateTo,
            Offset = request.Offset,
            Limit = request.Limit
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Добавить датчик к устройству
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPost("{deviceId}")]
    public async Task<BaseResponse<GetSensorResponse>> AddSensorAsync([FromRoute] Guid deviceId,[FromBody] AddSensorRequest request, CancellationToken ct)
    {
        var result = await _sensorsService.AddSensorAsync(new AddSensorParams()
        {
            DeviceId = deviceId,
            Name = request.Name,
            MeasurementSymbol = request.MeasurementSymbol
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Обновить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<BaseResponse<GetSensorResponse>> UpdateSensorAsync([FromRoute] Guid id, [FromBody] UpdateSensorRequest request, CancellationToken ct)
    {
        var result = await _sensorsService.UpdateSensorAsync(new UpdateSensorParams()
        {
            Id = id,
            MeasurementSymbol = request.MeasurementSymbol,
            Name = request.Name
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Удалить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<BaseResponse> DeleteSensorAsync([FromRoute] Guid id, CancellationToken ct)
    {
        await _sensorsService.DeleteSensorAsync(new DeleteSensorParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create();
    }
}