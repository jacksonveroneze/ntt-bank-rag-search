using NttBank.RagSearch.Infrastructure.Results;

namespace NttBank.RagSearch.Infrastructure.Services;

public interface IRagSearchService
{
    Task<RagSearchResult> SearchAsync(
        string query,
        int topK,
        CancellationToken cancellationToken);
}
