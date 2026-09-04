using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace NttBank.RagSearch.Infrastructure.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection AddMapper(
        this IServiceCollection services,
        Assembly assembly)
    {
        services.AddSingleton<TypeAdapterConfig>(_ =>
        {
            var config = new TypeAdapterConfig();
   
            config.Scan(assembly);
            
            return config;
        });

        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
