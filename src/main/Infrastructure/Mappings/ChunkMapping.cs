using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NttBank.RagSearch.Infrastructure.Entities;

namespace NttBank.RagSearch.Infrastructure.Mappings;

internal sealed class ChunkMapping
    : IEntityTypeConfiguration<Chunk>
{
    private const string TableName = "chunk";
    private const string EmbeddingColumnType = "vector(384)";
    private const string HnswIndexMethod = "hnsw";
    private const string VectorCosineOps = "vector_cosine_ops";
    private const string MStorageParameter = "m";
    private const int MStorageValue = 16;
    private const string EfConstructionStorageParameter = "ef_construction";
    private const int EfConstructionValue = 64;

    public void Configure(EntityTypeBuilder<Chunk> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable(TableName);

        builder.HasKey(conf => conf.Id);

        builder.Property(conf => conf.DocumentId)
            .IsRequired();

        builder.Property(conf => conf.Content)
            .IsRequired();

        builder.Property(conf => conf.Embedding)
            .HasColumnType(EmbeddingColumnType)
            .IsRequired();

        builder.Property(conf => conf.Metadata)
            .IsRequired();

        builder.Property(conf => conf.Index)
            .IsRequired();

        builder.Property(conf => conf.CreatedAt)
            .IsRequired();

        builder.HasIndex(e => e.Embedding)
            .HasMethod(HnswIndexMethod)
            .HasOperators(VectorCosineOps)
            .HasStorageParameter(MStorageParameter, MStorageValue)
            .HasStorageParameter(EfConstructionStorageParameter, EfConstructionValue);
    }
}
