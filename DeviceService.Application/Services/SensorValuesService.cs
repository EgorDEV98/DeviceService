using CommonLib.EFCore.Extensions;
using CommonLib.Exceptions;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Mappers;
using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Models.Response;
using DeviceService.Data;
using DeviceService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeviceService.Application.Services;

public class SensorValuesService : ISensorValuesService
{
    private readonly DeviceServiceDbContext _context;
    private readonly SensorValuesServiceMapper _mapper;

    public SensorValuesService(DeviceServiceDbContext context, SensorValuesServiceMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получить показание с датчика
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorValueResponse> GetSensorValueAsync(GetSensorValueParams param, CancellationToken ct)
    {
        var sensorValue = await _context.SensorValues
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (sensorValue is null) NotFoundException.Throw($"Sensor value Id({param.Id}) is not found!");
        return _mapper.Map(sensorValue!);
    }

    /// <summary>
    /// Получить список показаний датчика
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetSensorValueResponse>> GetSensorValuesAsync(GetSensorValuesParams param, CancellationToken ct)
    {
        var entities = await _context.SensorValues
            .AsNoTracking()
            .WhereIf(param.Ids is { Length: > 0 }, x => param.Ids!.Contains(x.Id))
            .WhereIf(param.SensorIds is { Length: > 0 }, x => param.SensorIds!.Contains(x.SensorId))
            .WhereIf(param.MeasurementDateFrom.HasValue, x => x.MeasurementDate >= param.MeasurementDateFrom)
            .WhereIf(param.MeasurementDateTo.HasValue, x => x.MeasurementDate <= param.MeasurementDateTo)
            .Skip(param.Offset ?? 0)
            .Take(param.Limit ?? 100)
            .ToArrayAsync(ct);
        return _mapper.Map(entities);
    }

    /// <summary>
    /// Добавить показание датчика
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetSensorValueResponse> AddSensorValueAsync(AddSensorValueParams param, CancellationToken ct)
    {
        var newSensorValue = new SensorValue()
        {
            MeasurementDate = param.MeasurementDate,
            SensorId = param.SensorId,
            Value = param.Value
        };

        await _context.SensorValues.AddAsync(newSensorValue, ct);
        await _context.SaveChangesAsync(ct);

        return _mapper.Map(newSensorValue);
    }

    /// <summary>
    /// Удалить показание по Id
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<bool> DeleteSensorValueAsync(DeleteSensorValueParams param, CancellationToken ct)
    {
        await _context.SensorValues.Where(x => param.Id == x.Id).ExecuteDeleteAsync(ct);
        return true;
    }

    /// <summary>
    /// Удалить все показания по Id
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<bool> TruncateSensorValuesAsync(TruncateSensorValuesParams param, CancellationToken ct)
    {
        await _context.SensorValues.Where(x => param.SensorId == x.SensorId).ExecuteDeleteAsync(ct);
        return true;
    }
}