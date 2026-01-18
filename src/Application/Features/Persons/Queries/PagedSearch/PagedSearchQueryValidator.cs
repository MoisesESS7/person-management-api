using FluentValidation;

namespace Application.Features.Persons.Queries.PagedSearch
{
    public sealed class PagedSearchQueryValidator : AbstractValidator<PagedSearchQuery>
    {
        public PagedSearchQueryValidator()
        {
            RuleFor(x => x.SearchParams)
                .NotNull()
                .WithMessage("Search parameters are required.")
                .ChildRules(search =>
                 {
                     search.RuleFor(x => x.PageNumber)
                         .GreaterThanOrEqualTo(1)
                         .WithMessage("PageNumber must be greater than or equal to 1.");

                     search.RuleFor(x => x.PageSize)
                         .InclusiveBetween(1, 25)
                         .WithMessage("PageSize must be between 1 and 25.");
                 });
        }
    }
}
