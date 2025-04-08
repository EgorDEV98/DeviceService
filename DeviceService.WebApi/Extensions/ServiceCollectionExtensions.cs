using CommonLib.Other.DateTimeProvider;
using DeviceService.Application.Interfaces;
using DeviceService.Application.Mappers;

namespace DeviceService.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDeviceService, Application.Services.DeviceService>();
        
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

        return serviceCollection;
    }
}