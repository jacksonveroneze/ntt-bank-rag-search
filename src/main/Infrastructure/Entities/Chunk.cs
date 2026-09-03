using Pgvector;

namespace NttBank.RagSearch.Infrastructure.Entities;

public sealed class Chunk
{
    public Guid Id { get; init; }

    public required Guid DocumentId { get; init; }

    public Document Document { get; init; } = null!;

    public required string Content { get; init; }

    public required Vector Embedding { get; init; }

    public string? Metadata { get; init; }

    public int Index { get; init; }

    public DateTime CreatedAt { get; init; }
}
