using PersonService.Application.Common.Validations.Extensions;
using FluentValidation;

namespace PersonService.Application.Features.Persons.Queries.GetById
{
    public sealed class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
    {
        public GetPersonByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .MustBeValidGuid();
        }
    }
}
