namespace NttBank.RagSearch.Infrastructure.Results;

public sealed record RagChunkResult(
    string Content,
    string? DocumentName,
    Uri? DocumentUrl,
    double? Score);
