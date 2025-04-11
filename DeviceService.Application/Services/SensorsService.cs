using CommonLib.EFCore.Extensions;
using CommonLib.Exceptions;
using CommonLib.Other.DateTimeProvider;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Mappers;
using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Models.Response;
using DeviceService.Data;
using DeviceService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Application.Services;

public class SensorsService : ISensorsService
{
    private readonly DeviceServiceDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly SensorServiceMapper _mapper;

    public SensorsService(DeviceServiceDbContext context, IDateTimeProvider dateTimeProvider, SensorServiceMapper mapper)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> GetSensorAsync(GetSensorParams param, CancellationToken ct)
    {
        var entity = await _context.Sensors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null) NotFoundException.Throw($"Sensor Id({param.Id}) is not found!");
        return _mapper.Map(entity!);
    }

    /// <summary>
    /// Получить список датчиков
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetSensorResponse>> GetSensorsAsync(GetSensorsParams param, CancellationToken ct)
    {
        var entities = await _context.Sensors
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
    /// Добавить датчик к устройству
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> AddSensorAsync(AddSensorParams param, CancellationToken ct)
    {
        var deviceIsExist = await _context.Devices.AnyAsync(x => x.Id == param.DeviceId, ct);
        if (!deviceIsExist) NotFoundException.Throw($"Device Id({param.DeviceId}) is not found!");

        var currentTime = _dateTimeProvider.GetCurrent();
        var sensor = new Sensor()
        {
            DeviceId = param.DeviceId,
            Name = param.Name,
            MeasurementSymbol = param.MeasurementSymbol,
            CreatedDate = currentTime,
            LastUpdate = currentTime
        };
        await _context.Sensors.AddAsync(sensor, ct);
        await _context.SaveChangesAsync(ct);

        return _mapper.Map(sensor);
    }

    /// <summary>
    /// Обновить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorResponse> UpdateSensorAsync(UpdateSensorParams param, CancellationToken ct)
    {
        var sensor = await _context.Sensors
            .FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (sensor is null) NotFoundException.Throw($"Sensor Id({param.Id}) is not found!");

        sensor!.MeasurementSymbol = param.MeasurementSymbol ?? sensor.MeasurementSymbol;
        sensor.Name = param.Name ?? sensor.Name;

        await _context.SaveChangesAsync(ct);
        return _mapper.Map(sensor);
    }

    /// <summary>
    /// Удалить датчик
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<bool> DeleteSensorAsync(DeleteSensorParams param, CancellationToken ct)
    {
        var sensor = await _context.Sensors
            .FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (sensor is null) NotFoundException.Throw($"Sensor Id({param.Id}) is not found!");
        
        _context.Sensors.Remove(sensor!);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}