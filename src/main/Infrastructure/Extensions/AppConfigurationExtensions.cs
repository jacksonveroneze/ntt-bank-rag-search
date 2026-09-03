using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NttBank.RagSearch.Infrastructure.Configurations;

namespace NttBank.RagSearch.Infrastructure.Extensions;

public static class AppConfigurationExtensions
{
    public static IServiceCollection AddAppConfigs(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddConfiguration<AppConfiguration>(configuration);

        services.AddSingleton(provider =>
            provider.GetRequiredService<AppConfiguration>().Database);

        services.AddSingleton(provider =>
            provider.GetRequiredService<AppConfiguration>().Embedding);

        return services;
    }

    private static IServiceCollection AddConfiguration<TParameterType>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? sectionName = null)
        where TParameterType : class
    {
        var section = string.IsNullOrEmpty(sectionName)
            ? configuration
            : configuration.GetSection(sectionName);

        services.AddOptions<TParameterType>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(provider =>
            provider.GetRequiredService<IOptions<TParameterType>>().Value);

        return services;
    }
}
