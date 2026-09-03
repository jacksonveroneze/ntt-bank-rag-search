using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NttBank.RagSearch.Infrastructure.Entities;

namespace NttBank.RagSearch.Infrastructure.Mappings;

internal sealed class DocumentMapping 
    : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("document");

        builder.HasKey(conf => conf.Id);

        builder.Property(conf => conf.Name)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(conf => conf.Source)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(conf => conf.Url)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(conf => conf.CreatedAt)
            .IsRequired();

        builder.HasMany(e => e.Chunks)
            .WithOne(c => c.Document)
            .HasForeignKey(c => c.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
