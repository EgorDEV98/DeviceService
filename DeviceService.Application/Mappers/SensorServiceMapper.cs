using DeviceService.Contracts.Models.Response;
using DeviceService.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace DeviceService.Application.Mappers;

[Mapper]
public partial class SensorServiceMapper
{
    [MapperIgnoreSource(nameof(Sensor.Device))]
    [MapperIgnoreSource(nameof(Sensor.DeviceId))]
    [MapperIgnoreSource(nameof(Sensor.SensorValues))]
    public partial GetSensorResponse Map(Sensor entity);
    public partial GetSensorResponse[] Map(Sensor[] entity);
}