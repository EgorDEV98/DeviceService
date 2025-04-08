using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Models.Response;

namespace DeviceService.Application.Interfaces;

public interface ISensorValuesService
{
    public Task<GetSensorValueResponse> GetSensorValueAsync(GetSensorValueParams param, CancellationToken ct);
    public Task<IReadOnlyCollection<GetSensorValueResponse>> GetSensorValuesAsync(GetSensorValuesParams param, CancellationToken ct);
    public Task<GetSensorValueResponse> AddSensorValueAsync(AddSensorValueParams param, CancellationToken ct);
    public Task<bool> DeleteSensorValueAsync(DeleteSensorValueParams param, CancellationToken ct);
    public Task<bool> TruncateSensorValuesAsync(TruncateSensorValuesParams param, CancellationToken ct);
}