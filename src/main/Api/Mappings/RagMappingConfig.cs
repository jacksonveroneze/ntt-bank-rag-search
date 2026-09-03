using Mapster;
using NttBank.RagSearch.Api.Contracts;
using NttBank.RagSearch.Infrastructure.Contracts;
using NttBank.RagSearch.Infrastructure.Results;

namespace NttBank.RagSearch.Api.Mappings;

public sealed class RagMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RagChunkResult, ChunkResponse>();

        config.NewConfig<RagSearchResult, SearchResponse>();

        config.NewConfig<IndexChunkRequest, IndexChunkInput>();

        config.NewConfig<IndexDocumentRequest, IndexDocumentInput>();
    }
}
