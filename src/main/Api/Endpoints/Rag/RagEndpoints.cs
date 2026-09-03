using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using NttBank.RagSearch.Api.Contracts;
using NttBank.RagSearch.Api.Endpoints.Extensions;
using NttBank.RagSearch.Infrastructure.Contracts;
using NttBank.RagSearch.Infrastructure.Services;

namespace NttBank.RagSearch.Api.Endpoints.Rag;

public static class RagEndpoints
{
    public static IEndpointRouteBuilder MapRagEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/rag")
            .WithTags("rag");

        group.MapPost("search", SearchAsync)
            .WithName("rag:search")
            .Produces<SearchResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("documents", IndexAsync)
            .WithName("rag:index-document")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }

    private static async Task<IResult> SearchAsync(
        [FromServices] IValidator<SearchRequest> validator,
        [FromServices] IRagSearchService service,
        [FromServices] IMapper mapper,
        [FromBody] SearchRequest request,
        CancellationToken cancellationToken)
    {
        var validation = await validator
            .ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            return validation.ToValidationProblem();
        }

        var result = await service.SearchAsync(
            request.Query,
            request.TopK,
            cancellationToken);

        var response = mapper.Map<SearchResponse>(result);

        return Results.Ok(response);
    }

    private static async Task<IResult> IndexAsync(
        [FromServices] IValidator<IndexDocumentRequest> validator,
        [FromServices] IRagIndexService service,
        [FromServices] IMapper mapper,
        [FromBody] IndexDocumentRequest request,
        CancellationToken cancellationToken)
    {
        var validation = await validator
            .ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
        {
            return validation.ToValidationProblem();
        }

        var input = mapper.Map<IndexDocumentInput>(request);
        
        var documentId = await service
            .IndexAsync(input, cancellationToken);

        return Results.Created(
            $"api/v1/rag/documents/{documentId}", 
            documentId);
    }
}
