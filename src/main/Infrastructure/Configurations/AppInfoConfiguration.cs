using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NttBank.RagSearch.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record AppInfoConfiguration
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required Version Version { get; init; }
}
