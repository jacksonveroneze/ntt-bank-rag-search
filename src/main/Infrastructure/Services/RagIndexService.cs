using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using NttBank.RagSearch.Infrastructure.Context;
using NttBank.RagSearch.Infrastructure.Contracts;
using NttBank.RagSearch.Infrastructure.Entities;
using NttBank.RagSearch.Infrastructure.Extensions;
using Pgvector;

namespace NttBank.RagSearch.Infrastructure.Services;

public sealed class RagIndexService(
    DefaultDbContext context,
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
    ILogger<RagIndexService> logger) : IRagIndexService
{
    public async Task<Guid> IndexAsync(
        IndexDocumentInput input,
        CancellationToken cancellationToken)
    {
        var document = new Document
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Source = input.Source,
            Url = input.Url,
            CreatedAt = DateTime.UtcNow,
        };

        var contents = input.Chunks.Select(c => c.Content).ToList();
        
        var embeddings = await embeddingGenerator.GenerateAsync(
            contents,
            cancellationToken: cancellationToken);

        if (embeddings.Count != contents.Count)
        {
            throw new InvalidOperationException(
                $"Embedding generator returned {embeddings.Count} embeddings " +
                $"for {contents.Count} contents.");
        }

        document.Chunks.AddRange(input.Chunks.Select((chunk, i) => new Chunk
        {
            Id = Guid.NewGuid(),
            DocumentId = document.Id,
            Content = chunk.Content,
            Embedding = new Vector(embeddings[i].Vector.ToArray()),
            Metadata = chunk.Metadata,
            Index = chunk.Index,
            CreatedAt = DateTime.UtcNow,
        }));

        await context.Documents.AddAsync(document, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        logger.DocumentIndexed(document.Id, document.Chunks.Count);

        return document.Id;
    }
}
