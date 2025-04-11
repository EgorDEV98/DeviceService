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
public class SensorValuesController : ControllerBase, ISensorValuesClient
{
    private readonly ISensorValuesService _service;
    private readonly SensorValuesControllerMapper _mapper;

    public SensorValuesController(ISensorValuesService service, SensorValuesControllerMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получить показание датчика
    /// </summary>
    /// <param name="id">Идентификатор показания</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<BaseResponse<GetSensorValueResponse>> GetSensorValueAsync([FromRoute] Guid id, CancellationToken ct)
        => AppResponse.Create(await _service.GetSensorValueAsync(new GetSensorValueParams()
        {
            Id = id
        }, ct));

    /// <summary>
    /// Получить список показаний датчиков
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<IReadOnlyCollection<GetSensorValueResponse>>> GetSensorValuesAsync([FromQuery] GetSensorValuesRequest request, CancellationToken ct)
        => AppResponse.Create(await _service.GetSensorValuesAsync(_mapper.Map(request), ct));

    /// <summary>
    /// Добавить показание к датчику
    /// </summary>
    /// <param name="sensorId">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPost("{sensorId}")]
    public async Task<BaseResponse<GetSensorValueResponse>> AddSensorValueAsync([FromRoute] Guid sensorId, [FromBody] AddSensorValueRequest request, CancellationToken ct)
        => AppResponse.Create(await _service.AddSensorValueAsync(new AddSensorValueParams()
        {
            SensorId = sensorId,
            Value = request.Value,
            MeasurementDate = request.MeasurementDate
        }, ct));

    /// <summary>
    /// Удалить показание по Id
    /// </summary>
    /// <param name="id">Идентификатор показания</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<BaseResponse> DeleteSensorValueAsync(Guid id, CancellationToken ct)
    {
        await _service.DeleteSensorValueAsync(new DeleteSensorValueParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create();
    }

    /// <summary>
    /// Массовое удаление показаний с параметрами
    /// </summary>
    /// <param name="sensorId">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpDelete("{sensorId}/params")]
    public async Task<BaseResponse> TruncateSensorValuesAsync(Guid sensorId, DeleteSensorValuesRequest request, CancellationToken ct)
    {
        await _service.TruncateSensorValuesAsync(new TruncateSensorValuesParams()
        {
            SensorId = sensorId,
            MeasurementDateFrom = request.MeasurementDateFrom,
            MeasurementDateTo = request.MeasurementDateTo
        }, ct);
        return AppResponse.Create();
    }
}