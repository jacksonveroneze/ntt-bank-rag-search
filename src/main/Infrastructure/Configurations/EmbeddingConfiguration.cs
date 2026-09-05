using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NttBank.RagSearch.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record EmbeddingConfiguration
{
    [Required]
    public required string Provider { get; init; }

    [Required]
    public required string Model { get; init; }

    [Required]
    public required Uri Endpoint { get; init; }

    public string? ApiKey { get; init; }

    public double MaxDistance { get; init; } = 0.3;
}
