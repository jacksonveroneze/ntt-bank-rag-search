using FluentValidation;
using NttBank.RagSearch.Api.Contracts;

namespace NttBank.RagSearch.Api.Validators;

public sealed class IndexDocumentRequestValidator
    : AbstractValidator<IndexDocumentRequest>
{
    private const int NameMaxLength = 500;

    public IndexDocumentRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(NameMaxLength);

        RuleFor(r => r.Chunks)
            .NotEmpty();

        RuleForEach(r => r.Chunks)
            .SetValidator(new IndexChunkRequestValidator());
    }
}
