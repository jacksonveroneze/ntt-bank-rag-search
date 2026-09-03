namespace NttBank.RagSearch.Api.Contracts;

public sealed record IndexDocumentRequest(
    string Name,
    string? Source,
    Uri? Url,
    IReadOnlyList<IndexChunkRequest> Chunks);

public sealed record IndexChunkRequest(
    string Content,
    int Index,
    string? Metadata);
