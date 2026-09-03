using System.Diagnostics.CodeAnalysis;

namespace NttBank.RagSearch.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppConfiguration
{
    public required AppInfoConfiguration Application { get; init; }
    
    public required DatabaseConfiguration Database { get; init; }
    
    public required EmbeddingConfiguration Embedding { get; init; }
    
    public required OpenTelemetryConfiguration OpenTelemetry { get; init; }
}
