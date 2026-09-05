using Microsoft.Extensions.Logging;

namespace NttBank.RagSearch.Infrastructure.Extensions;

internal static partial class LogMessagesExtensions
{
    [LoggerMessage(
        EventId = 1100,
        Level = LogLevel.Information,
        Message = "RAG search completed with {ResultCount} results")]
    public static partial void RagSearchCompleted(
        this ILogger logger,
        int resultCount);

    [LoggerMessage(
        EventId = 1102,
        Level = LogLevel.Information,
        Message = "Document {DocumentId} indexed with {ChunkCount} chunks")]
    public static partial void DocumentIndexed(
        this ILogger logger,
        Guid documentId,
        int chunkCount);
}
