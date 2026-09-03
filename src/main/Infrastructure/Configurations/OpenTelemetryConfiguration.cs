using System.Diagnostics.CodeAnalysis;

namespace NttBank.RagSearch.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record OpenTelemetryConfiguration
{
    public Uri? EndpointTracing { get; init; }
}
