using Microsoft.Extensions.DependencyInjection;
using NttBank.RagSearch.Infrastructure.Services;

namespace NttBank.RagSearch.Infrastructure.Extensions;

public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IRagSearchService, RagSearchService>();
        services.AddScoped<IRagIndexService, RagIndexService>();

        return services;
    }
}
