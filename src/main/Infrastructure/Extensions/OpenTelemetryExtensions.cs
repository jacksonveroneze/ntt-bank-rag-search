using Microsoft.Extensions.DependencyInjection;
using NttBank.RagSearch.Infrastructure.Configurations;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace NttBank.RagSearch.Infrastructure.Extensions;

public static class OpenTelemetryExtensions
{
    private static readonly string[] ExclusionPathsTrace =
    [
        "/metrics",
        "/health",
        "/health/live",
        "/health/ready",
    ];

    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.Configure<AspNetCoreTraceInstrumentationOptions>(options =>
        {
            options.Filter = ctx => !ExclusionPathsTrace
                .Contains(ctx.Request.Path.Value);
        });

        services.AddOpenTelemetry()
            .ConfigureResource(ConfigureResource)
            .AddMetrics()
            .AddTracing(appConfiguration);

        return services;

        void ConfigureResource(ResourceBuilder r)
        {
            r.AddService(
                appConfiguration.Application.Name,
                serviceVersion: appConfiguration.Application.Version.ToString(),
                serviceInstanceId: Environment.MachineName);
        }
    }

    public static IOpenTelemetryBuilder AddMetrics(this IOpenTelemetryBuilder builder)
    {
        builder.WithMetrics(options => options
            .AddProcessInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddMeter("Microsoft.Extensions.AI")
            .AddPrometheusExporter());

        return builder;
    }

    public static IOpenTelemetryBuilder AddTracing(
        this IOpenTelemetryBuilder builder,
        AppConfiguration appConfiguration)
    {
        if (appConfiguration.OpenTelemetry.EndpointTracing is null)
        {
            return builder;
        }

        builder.WithTracing(options =>
        {
            options
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource("Microsoft.Extensions.AI")
                .AddOtlpExporter(config =>
                    config.Endpoint = appConfiguration.OpenTelemetry.EndpointTracing);
        });

        return builder;
    }
}
