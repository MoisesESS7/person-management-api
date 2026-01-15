using Application.Common.Validations.Extensions;
using FluentValidation;

namespace Application.Features.Persons.Commands.Create
{
    public sealed class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.Name)
                .MustBeValidName();

            RuleFor(x => x.BirthDate)
                .MustBeValidBirthDate();

            RuleFor(x => x.CpfNumber)
                .MustBeValidCpfNumber();

            RuleFor(x => x.CpfRegistrationDate)
                .MustBeValidCpfRegistrationDate(x => x.BirthDate);

            RuleFor(x => x.RgNumber)
                .MustBeValidRgNumber();

            RuleFor(x => x.RgIssuingAuthority)
                .MustBeValidRgIssuingAuthority();
        }
    }
}
