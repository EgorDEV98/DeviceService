using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Models.Response;

namespace DeviceService.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса актуаторов
/// </summary>
public interface IActuatorsService
{
    public Task<GetActuatorResponse> GetActuatorAsync(GetActuatorParams param, CancellationToken ct);
    public Task<IReadOnlyCollection<GetActuatorResponse>> GetActuatorsAsync(GetActuatorsParams param, CancellationToken ct);
    public Task<GetActuatorResponse> AddActuatorAsync(AddActuatorParams param, CancellationToken ct);
    public Task<GetActuatorResponse> UpdateActuatorAsync(UpdateActuatorParams param, CancellationToken ct);
    public Task<bool> DeleteAsync(DeleteActuatorParams param, CancellationToken ct);
}