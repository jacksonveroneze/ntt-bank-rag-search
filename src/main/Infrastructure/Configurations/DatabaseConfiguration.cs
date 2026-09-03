using System.Diagnostics.CodeAnalysis;

namespace NttBank.RagSearch.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record DatabaseConfiguration
{
    public required string ConnectionString { get; init; }
}
