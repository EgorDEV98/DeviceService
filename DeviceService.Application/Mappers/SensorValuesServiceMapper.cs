using DeviceService.Contracts.Models.Response;
using DeviceService.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace DeviceService.Application.Mappers;

[Mapper]
public partial class SensorValuesServiceMapper
{
    public partial GetSensorValueResponse Map(SensorValue entity);
    public partial GetSensorValueResponse[] Map(SensorValue[] entity);
}