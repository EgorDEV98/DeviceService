using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Models.Response;

namespace DeviceService.Application.Interfaces;

public interface IDevicesService
{
    public Task<GetDeviceResponse> GetDeviceAsync(GetDeviceParams param, CancellationToken ct);
    public Task<IReadOnlyCollection<GetDeviceResponse>> GetDevicesAsync(GetDevicesParams param, CancellationToken ct);
    public Task<GetDeviceResponse> AddDeviceAsync(AddDeviceParams param, CancellationToken ct);
    public Task<GetDeviceResponse> UpdateDeviceAsync(UpdateDeviceParams param, CancellationToken ct);
    public Task<bool> DeleteDeviceAsync(DeleteDeviceParams param, CancellationToken ct);
}