using CommonLib.Response;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Clients;
using DeviceService.Contracts.Models.Request;
using DeviceService.Contracts.Models.Response;
using DeviceService.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorsController : ControllerBase, ISensorsClient
{
    private readonly ISensorsService _service;
    private readonly SensorControllerMapper _mapper;

    public SensorsController(ISensorsService service, SensorControllerMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<BaseResponse<GetSensorResponse>> GetSensorAsync([FromRoute] Guid id, CancellationToken ct)
        => AppResponse.Create(await _service.GetSensorAsync(new GetSensorParams()
            {
                Id = id
            }, ct));

    /// <summary>
    /// Получить список датчиков
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<IReadOnlyCollection<GetSensorResponse>>> GetSensorsResponse([FromQuery] GetSensorsRequest request, CancellationToken ct)
        => AppResponse.Create(await _service.GetSensorsAsync(_mapper.Map(request), ct));

    /// <summary>
    /// Добавить датчик к устройству
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPost("{deviceId}")]
    public async Task<BaseResponse<GetSensorResponse>> AddSensorAsync([FromRoute] Guid deviceId, [FromBody] AddSensorRequest request, CancellationToken ct)
        => AppResponse.Create(await _service.AddSensorAsync(new AddSensorParams()
        {
            DeviceId = deviceId,
            Name = request.Name,
            MeasurementSymbol = request.MeasurementSymbol
        }, ct));

    /// <summary>
    /// Обновить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<BaseResponse<GetSensorResponse>> UpdateSensorAsync([FromRoute] Guid id, [FromBody] UpdateSensorRequest request, CancellationToken ct)
        => AppResponse.Create(await _service.UpdateSensorAsync(new UpdateSensorParams()
        {
            Id = id,
            MeasurementSymbol = request.MeasurementSymbol,
            Name = request.Name
        }, ct));

    /// <summary>
    /// Удалить датчик
    /// </summary>
    /// <param name="id">Идентификатор датчика</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<BaseResponse> DeleteSensorAsync([FromRoute] Guid id, CancellationToken ct)
    {
        await _service.DeleteSensorAsync(new DeleteSensorParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create();
    }
}