namespace NttBank.RagSearch.Infrastructure.Results;

public sealed record RagSearchResult(
    IReadOnlyList<RagChunkResult> Results);
