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
using NotImplementedException = System.NotImplementedException;

namespace DeviceService.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly DeviceServiceDbContext _context;
    private readonly DeviceServiceMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeviceService(DeviceServiceDbContext context, DeviceServiceMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }
    
    /// <summary>
    /// Получить устройство по Id
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetDeviceResponse> GetDeviceAsync(GetDeviceParams param, CancellationToken ct)
    {
        var entity = await _context.Devices
            .AsNoTracking()
            .Include(x => x.Actuators)
            .Include(x => x.Sensors)
            .FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null) NotFoundException.Throw($"Device Id({param.Id}) is not found!");
        return _mapper.Map(entity!);
    }

    /// <summary>
    /// Получить список устройств
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetDeviceResponse>> GetDevicesAsync(GetDevicesParams param, CancellationToken ct)
    {
        var entities = await _context.Devices
            .AsNoTracking()
            .Include(x => x.Actuators)
            .Include(x => x.Sensors)
            .WhereIf(param.Ids is { Length: > 0 }, x => param.Ids!.Contains(x.Id))
            .WhereIf(param.UserIds is { Length: > 0 }, x => param.UserIds!.Contains(x.UserId))
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
    /// Добавить новое устройство
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetDeviceResponse> AddDeviceAsync(AddDeviceParams param, CancellationToken ct)
    {
        var newDevice = new Device()
        {
            UserId = param.UserId,
            Name = param.Name,
            CreatedDate = _dateTimeProvider.GetCurrent(),
            LastUpdate = _dateTimeProvider.GetCurrent()
        };

        await _context.Devices.AddAsync(newDevice, ct);
        await _context.SaveChangesAsync(ct);

        return _mapper.Map(newDevice);
    }

    /// <summary>
    /// Обновить устройство
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetDeviceResponse> UpdateDeviceAsync(UpdateDeviceParams param, CancellationToken ct)
    {
        var entity = await _context.Devices.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null) NotFoundException.Throw($"Device Id({param.Id}) is not found");

        entity!.Name = param.Name ?? entity!.Name;
        await _context.SaveChangesAsync(ct);

        return _mapper.Map(entity);
    }

    /// <summary>
    /// Удалить устройство
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<bool> DeleteDeviceAsync(DeleteDeviceParams param, CancellationToken ct)
    {
        var entity = await _context.Devices.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (entity is null) NotFoundException.Throw($"Device Id({param.Id}) is not found");

        _context.Devices.Remove(entity!);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}