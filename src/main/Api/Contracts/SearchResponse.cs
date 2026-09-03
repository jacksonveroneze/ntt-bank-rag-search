namespace NttBank.RagSearch.Api.Contracts;

public sealed record SearchResponse(
    IReadOnlyList<ChunkResponse> Results);
