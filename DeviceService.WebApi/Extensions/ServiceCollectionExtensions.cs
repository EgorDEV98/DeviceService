using CommonLib.Other.DateTimeProvider;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Mappers;
using DeviceService.Application.Services;

namespace DeviceService.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDevicesService, DevicesService>();
        serviceCollection.AddScoped<IActuatorsService, ActuatorsService>();
        serviceCollection.AddScoped<ISensorsService, SensorsService>();
        serviceCollection.AddScoped<ISensorValuesService, SensorValuesService>();
        
        return serviceCollection;
    }
    public static IServiceCollection AddCommon(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return serviceCollection;
    }
    
    public static IServiceCollection AddMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<DeviceServiceMapper>();
        serviceCollection.AddSingleton<ActuatorServiceMapper>();
        serviceCollection.AddSingleton<SensorServiceMapper>();
        serviceCollection.AddSingleton<SensorValuesServiceMapper>();

        return serviceCollection;
    }
}