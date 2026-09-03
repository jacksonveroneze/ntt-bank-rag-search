using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NttBank.RagSearch.Infrastructure.Entities;
using NttBank.RagSearch.Infrastructure.Mappings;

namespace NttBank.RagSearch.Infrastructure.Context;

[ExcludeFromCodeCoverage]
public class DefaultDbContext(
    DbContextOptions<DefaultDbContext> options)
    : DbContext(options)
{
    public DbSet<Document> Documents => Set<Document>();

    public DbSet<Chunk> Chunks => Set<Chunk>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema("rag_schema");
        modelBuilder.HasPostgresExtension("rag_schema", "vector");

        modelBuilder.ApplyConfiguration(new ChunkMapping());
        modelBuilder.ApplyConfiguration(new DocumentMapping());
    }
}
