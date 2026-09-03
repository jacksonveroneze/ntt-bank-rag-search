namespace NttBank.RagSearch.Infrastructure.Entities;

public sealed class Document
{
    public Guid Id { get; init; }

    public required string Name { get; set; }

    public string? Source { get; set; }

    public Uri? Url { get; set; }

    public DateTime CreatedAt { get; init; }

    public List<Chunk> Chunks { get; init; } = [];
}
