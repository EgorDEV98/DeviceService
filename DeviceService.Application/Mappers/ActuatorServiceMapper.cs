using DeviceService.Contracts.Models.Response;
using DeviceService.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace DeviceService.Application.Mappers;

[Mapper]
public partial class ActuatorServiceMapper
{
    [MapperIgnoreSource(nameof(Actuator.Device))]
    [MapperIgnoreSource(nameof(Actuator.DeviceId))]
    public partial GetActuatorResponse Map(Actuator entity);
    public partial GetActuatorResponse[] Map(Actuator[] entities);
}