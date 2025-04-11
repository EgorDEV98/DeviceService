using DeviceService.Application.Models.Params;
using DeviceService.Contracts.Models.Request;
using Riok.Mapperly.Abstractions;

namespace DeviceService.WebApi.Mappers;

[Mapper]
public partial class DeviceControllerMapper
{
    public partial GetDevicesParams Map(GetDevicesRequest request);
}