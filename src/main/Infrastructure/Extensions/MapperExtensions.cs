using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace NttBank.RagSearch.Infrastructure.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection AddMapper(
        this IServiceCollection services)
    {
        services.AddSingleton<TypeAdapterConfig>(_ =>
        {
            var config = new TypeAdapterConfig();
            //new RagMappingConfig().Register(config);
            return config;
        });

        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
