using DeviceService.Contracts.Models.Response;
using DeviceService.Data.Entities;
using Riok.Mapperly.Abstractions;

namespace DeviceService.Application.Mappers;

[Mapper]
public partial class DeviceServiceMapper
{ 
    
    public partial GetDeviceResponse Map(Device entity);
    public partial GetDeviceResponse[] Map(Device[] entity);
}