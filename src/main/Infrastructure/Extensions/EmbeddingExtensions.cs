using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using NttBank.RagSearch.Infrastructure.Configurations;
using OllamaSharp;

namespace NttBank.RagSearch.Infrastructure.Extensions;

public static class EmbeddingExtensions
{
    public static IServiceCollection AddEmbeddingGenerator(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddSingleton<IEmbeddingGenerator<string, Embedding<float>>>(provider =>
        {
            var configuration = appConfiguration.Embedding;

            return configuration.Provider.ToLowerInvariant() switch
            {
                "ollama" => new OllamaApiClient(
                    configuration.Endpoint, configuration.Model),

                _ => throw new NotSupportedException(
                    $"Embedding provider '{configuration.Provider}' is not supported."),
            };
        });

        return services;
    }
}
