using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Models.Response;

namespace DeviceService.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса датчиков
/// </summary>
public interface ISensorsService
{
    public Task<GetSensorResponse> GetSensorAsync(GetSensorParams param, CancellationToken ct);
    public Task<IReadOnlyCollection<GetSensorResponse>> GetSensorsAsync(GetSensorsParams param, CancellationToken ct);
    public Task<GetSensorResponse> AddSensorAsync(AddSensorParams param, CancellationToken ct);
    public Task<GetSensorResponse> UpdateSensorAsync(UpdateSensorParams param, CancellationToken ct);
    public Task<bool> DeleteSensorAsync(DeleteSensorParams param, CancellationToken ct);
}