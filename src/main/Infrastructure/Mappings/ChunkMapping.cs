using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NttBank.RagSearch.Infrastructure.Entities;

namespace NttBank.RagSearch.Infrastructure.Mappings;

internal sealed class ChunkMapping 
    : IEntityTypeConfiguration<Chunk>
{
    public void Configure(EntityTypeBuilder<Chunk> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("chunk");

        builder.HasKey(conf => conf.Id);

        builder.Property(conf => conf.DocumentId)
            .IsRequired();

        builder.Property(conf => conf.Content)
            .IsRequired();

        builder.Property(conf => conf.Embedding)
            .HasColumnType("vector(384)")
            .IsRequired();

        builder.Property(conf => conf.Metadata)
            .IsRequired();

        builder.Property(conf => conf.Index)
            .IsRequired();

        builder.Property(conf => conf.CreatedAt)
            .IsRequired();

        builder.HasIndex(e => e.Embedding)
            .HasMethod("hnsw")
            .HasOperators("vector_cosine_ops")
            .HasStorageParameter("m", 16)
            .HasStorageParameter("ef_construction", 64);
    }
}
