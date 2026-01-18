using Application.Common.Validations.Extensions;
using FluentValidation;

namespace Application.Features.Persons.Commands.Delete
{
    public sealed class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator()
        {
            RuleFor(x => x.Id)
                .MustBeValidGuid();
        }
    }
}
