using FluentValidation;
using NttBank.RagSearch.Api.Contracts;

namespace NttBank.RagSearch.Api.Validators;

public sealed class IndexChunkRequestValidator
    : AbstractValidator<IndexChunkRequest>
{
    private const int ContentMaxLength = 100000;
    private const int MinIndexValue = 0;

    public IndexChunkRequestValidator()
    {
        RuleFor(r => r.Content)
            .NotEmpty()
            .MaximumLength(ContentMaxLength);

        RuleFor(r => r.Index)
            .GreaterThanOrEqualTo(MinIndexValue);
    }
}
