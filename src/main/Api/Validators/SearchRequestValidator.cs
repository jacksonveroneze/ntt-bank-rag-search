using FluentValidation;
using NttBank.RagSearch.Api.Contracts;

namespace NttBank.RagSearch.Api.Validators;

public sealed class SearchRequestValidator
    : AbstractValidator<SearchRequest>
{
    private const int QueryMaxLength = 4000;
    private const int TopKMinValue = 1;

    private const int TopKMaxValue = 40;

    public SearchRequestValidator()
    {
        RuleFor(r => r.Query)
            .NotEmpty()
            .MaximumLength(QueryMaxLength);

        RuleFor(r => r.TopK)
            .InclusiveBetween(TopKMinValue, TopKMaxValue);
    }
}
