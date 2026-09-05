using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NttBank.RagSearch.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record DatabaseConfiguration
{
    [Required]
    public required string ConnectionString { get; init; }
}
