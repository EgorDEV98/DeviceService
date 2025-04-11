using CommonLib.EFCore.Extensions;
using CommonLib.Exceptions;
using CommonLib.Other.DateTimeProvider;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Mappers;
using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Enums;
using DeviceService.Contracts.Models.Response;
using DeviceService.Data;
using DeviceService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Application.Services;

public class ActuatorsService : IActuatorsService
{
    private readonly DeviceServiceDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ActuatorServiceMapper _mapper;

    public ActuatorsService(DeviceServiceDbContext context, IDateTimeProvider  dateTimeProvider, ActuatorServiceMapper mapper)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получить конкретный актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetActuatorResponse> GetActuatorAsync(GetActuatorParams param, CancellationToken ct)
    {
        var entity = await _context.Actuators
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null) NotFoundException.Throw($"Actuator Id({param.Id}) is not found!");
        return _mapper.Map(entity!);
    }

    /// <summary>
    /// Получить список актуаторов
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetActuatorResponse>> GetActuatorsAsync(GetActuatorsParams param, CancellationToken ct)
    {
        var entities = await _context.Actuators
            .AsNoTracking()
            .Include(x => x.Device)
            .WhereIf(param.Ids is { Length: > 0 }, x => param.Ids!.Contains(x.Id))
            .WhereIf(param.DeviceIds is { Length: > 0}, x => param.DeviceIds!.Contains(x.DeviceId))
            .WhereIf(param.UserIds is { Length: > 0 }, x => param.UserIds!.Contains(x.Device!.UserId))
            .WhereIf(param.CreatedDateFrom.HasValue, x => x.CreatedDate >= param.CreatedDateFrom)
            .WhereIf(param.CreatedDateTo.HasValue, x => x.CreatedDate <= param.CreatedDateTo)
            .WhereIf(param.LastUpdateFrom.HasValue, x => x.LastUpdate >= param.LastUpdateFrom)
            .WhereIf(param.LastUpdateTo.HasValue, x => x.LastUpdate <= param.LastUpdateTo)
            .Skip(param.Offset ?? 0)
            .Take(param.Limit ?? 100)
            .ToArrayAsync(ct);
        return _mapper.Map(entities);
    }

    /// <summary>
    /// Добавить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public async Task<GetActuatorResponse> AddActuatorAsync(AddActuatorParams param, CancellationToken ct)
    {
        var isExistDevice = await _context.Devices.AnyAsync(x => x.Id == param.DeviceId, ct);
        if (!isExistDevice) NotFoundException.Throw($"Device Id({param.DeviceId}) is not found!");

        var currentTime = _dateTimeProvider.GetCurrent();
        var actuator = new Actuator()
        {
            DeviceId = param!.DeviceId,
            Name = param.Name,
            State = ActuatorState.Disable,
            CreatedDate = currentTime,
            LastUpdate = currentTime
        };
        await _context.Actuators.AddAsync(actuator, ct);
        await _context.SaveChangesAsync(ct);

        return _mapper.Map(actuator);
    }

    /// <summary>
    /// Обновить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetActuatorResponse> UpdateActuatorAsync(UpdateActuatorParams param, CancellationToken ct)
    {
        var entity = await _context.Actuators.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null) NotFoundException.Throw($"Actuator Id({param.Id}) is not found!");

        entity!.State = param.State ?? entity.State;
        entity.Name = param.Name ?? entity.Name;
        await _context.SaveChangesAsync(ct);

        return _mapper.Map(entity);
    }

    /// <summary>
    /// Удалить актуатор
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(DeleteActuatorParams param, CancellationToken ct)
    {
        var entity = await _context.Actuators.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null) NotFoundException.Throw($"Actuator Id({param.Id}) is not found!");
        
        _context.Actuators.Remove(entity!);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}