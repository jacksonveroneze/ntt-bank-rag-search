namespace NttBank.RagSearch.Api.Contracts;

public sealed record SearchRequest(
    string Query,
    int TopK);
