using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using NttBank.RagSearch.Infrastructure.Configurations;
using NttBank.RagSearch.Infrastructure.Context;
using NttBank.RagSearch.Infrastructure.Extensions;
using NttBank.RagSearch.Infrastructure.Results;
using Pgvector;
using Pgvector.EntityFrameworkCore;

namespace NttBank.RagSearch.Infrastructure.Services;

public sealed class RagSearchService(
    DefaultDbContext context,
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
    EmbeddingConfiguration embeddingConfiguration,
    ILogger<RagSearchService> logger) : IRagSearchService
{
    public async Task<RagSearchResult> SearchAsync(
        string query,
        int topK,
        CancellationToken cancellationToken)
    {
        var embeddings = await embeddingGenerator.GenerateAsync(
            [query],
            cancellationToken: cancellationToken);

        var embedding = embeddings.FirstOrDefault()
            ?? throw new InvalidOperationException(
                "Embedding generation returned no result.");

        var queryVector = new Vector(
            embedding.Vector.ToArray());

        var chunks = await context.Chunks
            .AsNoTracking()
            .Include(c => c.Document)
            .Where(c => c.Embedding.CosineDistance(queryVector) 
                        <= embeddingConfiguration.MaxDistance)
            .OrderBy(c => c.Embedding.CosineDistance(queryVector))
            .Take(topK)
            .Select(selector => new
            {
                selector.Content,
                selector.Document.Name,
                selector.Document.Url,
                Distance = selector.Embedding.CosineDistance(queryVector),
            })
            .ToListAsync(cancellationToken);

        var results = chunks
            .Select(selector => new RagChunkResult(
                selector.Content,
                selector.Name,
                selector.Url,
                1.0 - selector.Distance))
            .ToList();

        logger.RagSearchCompleted(results.Count);

        return new RagSearchResult(results);
    }
}
