namespace NttBank.RagSearch.Infrastructure.Contracts;

public sealed record IndexDocumentInput(
    string Name,
    string? Source,
    Uri? Url,
    IReadOnlyList<IndexChunkInput> Chunks);
