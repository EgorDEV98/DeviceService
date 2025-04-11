using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Models.Request;
using Riok.Mapperly.Abstractions;

namespace DeviceService.WebApi.Mappers;

[Mapper]
public partial class ActuatorControllerMapper
{
    public partial GetActuatorsParams Map(GetActuatorsRequest request);
}