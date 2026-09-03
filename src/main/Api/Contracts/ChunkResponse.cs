namespace NttBank.RagSearch.Api.Contracts;

public sealed record ChunkResponse(
    string Content,
    string? DocumentName,
    Uri? DocumentUrl,
    double? Score);
