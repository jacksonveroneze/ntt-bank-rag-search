using Mapster;
using NttBank.RagSearch.Api.Contracts;
using NttBank.RagSearch.Infrastructure.Contracts;
using NttBank.RagSearch.Infrastructure.Results;

namespace NttBank.RagSearch.Api.Mappings;

public sealed class RagMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RagChunkResult, ChunkResponse>()
            .Map(dest => dest.Content, src => src.Content)
            .Map(dest => dest.DocumentName, src => src.DocumentName)
            .Map(dest => dest.DocumentUrl, src => src.DocumentUrl)
            .Map(dest => dest.Score, src => src.Score);

        config.NewConfig<RagSearchResult, SearchResponse>()
            .Map(dest => dest.Results, src => src.Results);

        config.NewConfig<IndexChunkRequest, IndexChunkInput>()
            .Map(dest => dest.Content, src => src.Content)
            .Map(dest => dest.Index, src => src.Index)
            .Map(dest => dest.Metadata, src => src.Metadata);

        config.NewConfig<IndexDocumentRequest, IndexDocumentInput>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Source, src => src.Source)
            .Map(dest => dest.Url, src => src.Url)
            .Map(dest => dest.Chunks, src => src.Chunks);
    }
}
