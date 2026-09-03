using NttBank.RagSearch.Infrastructure.Contracts;

namespace NttBank.RagSearch.Infrastructure.Services;

public interface IRagIndexService
{
    Task<Guid> IndexAsync(
        IndexDocumentInput input,
        CancellationToken cancellationToken);
}
