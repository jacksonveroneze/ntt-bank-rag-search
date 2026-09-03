namespace NttBank.RagSearch.Infrastructure.Contracts;

public sealed record IndexChunkInput(
    string Content,
    int Index,
    string? Metadata);
