using Application.Common.Validations.Extensions;
using FluentValidation;

namespace Application.Features.Persons.Commands.Update
{
    public sealed class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Id)
                .MustBeValidGuid();

            RuleFor(x => x.Name)
                .MustBeValidName();
        }
    }
}
