using System.Diagnostics.CodeAnalysis;

namespace NttBank.RagSearch.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record EmbeddingConfiguration
{
    public required string Provider { get; init; }

    public required string Model { get; init; }

    public required Uri Endpoint { get; init; }

    public string? ApiKey { get; init; }

    public int Dimensions { get; init; } = 1536;

    public double MaxDistance { get; init; } = 0.3;
}
