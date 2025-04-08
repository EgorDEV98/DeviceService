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
public class SensorValuesController : ControllerBase, ISensorValuesClient
{
    private readonly ISensorValuesService _service;

    public SensorValuesController(ISensorValuesService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Получить показание датчика
    /// </summary>
    /// <param name="id">Идентификатор показания</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<BaseResponse<GetSensorValueResponse>> GetSensorValueAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await _service.GetSensorValueAsync(new GetSensorValueParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Получить список показаний датчиков
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<IReadOnlyCollection<GetSensorValueResponse>>> GetSensorValuesAsync([FromQuery] GetSensorValuesRequest request, CancellationToken ct)
    {
        var result = await _service.GetSensorValuesAsync(new GetSensorValuesParams()
        {
            Ids = request.Ids,
            SensorIds = request.SensorIds,
            MeasurementDateFrom = request.MeasurementDateFrom,
            MeasurementDateTo = request.MeasurementDateTo,
            Offset = request.Offset,
            Limit = request.Limit
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Добавить показание к датчику
    /// </summary>
    /// <param name="sensorId">Идентификатор датчика</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPost("{sensorId}")]
    public async Task<BaseResponse<GetSensorValueResponse>> AddSensorValueAsync([FromRoute] Guid sensorId, [FromBody] AddSensorValueRequest request, CancellationToken ct)
    {
        var result = await _service.AddSensorValueAsync(new AddSensorValueParams()
        {
            SensorId = sensorId,
            Value = request.Value,
            MeasurementDate = request.MeasurementDate
        }, ct);
        return AppResponse.Create(result);
    }

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
        return AppResponse.Create(id);
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