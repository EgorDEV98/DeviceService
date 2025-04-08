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
public class ActuatorsController : ControllerBase, IActuatorsClient
{
    private readonly IActuatorsService _service;

    public ActuatorsController(IActuatorsService service)
    {
        _service = service;
    }
    
    /// <summary>
    /// Получить конкретный актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<BaseResponse<GetActuatorResponse>> GetActuatorAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await _service.GetActuatorAsync(new GetActuatorParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Получить список актуаторов
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<IReadOnlyCollection<GetActuatorResponse>>> GetActuatorsAsync([FromQuery] GetActuatorsRequest request, CancellationToken ct)
    {
        var result = await _service.GetActuatorsAsync(new GetActuatorsParams()
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
    /// Добавить актуатор к устройству
    /// </summary>
    /// <param name="deviceId">Идентификатор устройства</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public async Task<BaseResponse<GetActuatorResponse>> AddActuatorAsync([FromRoute] Guid deviceId, [FromBody] AddActuatorRequest request, CancellationToken ct)
    {
        var result = await _service.AddActuatorAsync(new AddActuatorParams()
        {
            DeviceId = deviceId,
            Name = request.Name
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Обновить актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<BaseResponse<GetActuatorResponse>> UpdateActuatorAsync([FromRoute] Guid id,[FromBody] UpdateActuatorRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateActuatorAsync(new UpdateActuatorParams()
        {
            Id = id,
            Name = request.Name,
            State = request.State
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Удалить актуатор
    /// </summary>
    /// <param name="id">Идентификатор актуатора</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<BaseResponse> DeleteActuatorAsync(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(new DeleteActuatorParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create();
    }
}