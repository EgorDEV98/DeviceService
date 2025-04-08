using DeviceService.Contracts.Models.Response;
using DeviceService.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace DeviceService.Application.Mappers;

[Mapper]
public partial class SensorServiceMapper
{
    public partial GetSensorResponse Map(Sensor entity);
    public partial GetSensorResponse[] Map(Sensor[] entity);
}