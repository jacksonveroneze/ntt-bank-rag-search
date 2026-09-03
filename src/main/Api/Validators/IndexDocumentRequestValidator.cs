using FluentValidation;
using NttBank.RagSearch.Api.Contracts;

namespace NttBank.RagSearch.Api.Validators;

public sealed class IndexDocumentRequestValidator
    : AbstractValidator<IndexDocumentRequest>
{
    public IndexDocumentRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(r => r.Chunks)
            .NotEmpty();

        RuleForEach(r => r.Chunks)
            .SetValidator(new IndexChunkRequestValidator());
    }
}

public sealed class IndexChunkRequestValidator
    : AbstractValidator<IndexChunkRequest>
{
    public IndexChunkRequestValidator()
    {
        RuleFor(r => r.Content)
            .NotEmpty()
            .MaximumLength(100000);

        RuleFor(r => r.Index)
            .GreaterThanOrEqualTo(0);
    }
}
