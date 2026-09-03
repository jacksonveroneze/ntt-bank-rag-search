using FluentValidation;
using NttBank.RagSearch.Api.Contracts;

namespace NttBank.RagSearch.Api.Validators;

public sealed class SearchRequestValidator 
    : AbstractValidator<SearchRequest>
{
    public SearchRequestValidator()
    {
        RuleFor(r => r.Query)
            .NotEmpty()
            .MaximumLength(4000);

        RuleFor(r => r.TopK)
            .InclusiveBetween(1, 100);
    }
}
