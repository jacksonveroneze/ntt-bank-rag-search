using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Pgvector;

#nullable disable

namespace NttBank.RagSearch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rag_schema");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:rag_schema.vector", ",,");

            migrationBuilder.CreateTable(
                name: "document",
                schema: "rag_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    source = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chunk",
                schema: "rag_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    embedding = table.Column<Vector>(type: "vector(384)", nullable: false),
                    metadata = table.Column<string>(type: "text", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chunk", x => x.id);
                    table.ForeignKey(
                        name: "fk_chunk_documents_document_id",
                        column: x => x.document_id,
                        principalSchema: "rag_schema",
                        principalTable: "document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_chunk_document_id",
                schema: "rag_schema",
                table: "chunk",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "ix_chunk_embedding",
                schema: "rag_schema",
                table: "chunk",
                column: "embedding")
                .Annotation("Npgsql:IndexMethod", "hnsw")
                .Annotation("Npgsql:IndexOperators", new[] { "vector_cosine_ops" })
                .Annotation("Npgsql:StorageParameter:ef_construction", 64)
                .Annotation("Npgsql:StorageParameter:m", 16);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chunk",
                schema: "rag_schema");

            migrationBuilder.DropTable(
                name: "document",
                schema: "rag_schema");
        }
    }
}
