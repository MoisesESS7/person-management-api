using PersonService.Domain.Exceptions.Documents;
using PersonService.Domain.ValueObjects.Bases;
using PersonService.Shared.Enums;

namespace PersonService.Domain.ValueObjects
{
    public sealed class Rg : Document
    {
        public IssuingAuthority IssuingAuthority { get; private set; }

        public Rg(string number, DateTimeOffset birthDate, IssuingAuthority issuingAuthority) : base(number, birthDate)
        {
            IssuingAuthority = issuingAuthority;
            Validation();
        }
        private void Validation()
        {
            var errors = new List<string>();

            if (Number.Length != 9)
                errors.Add("The RG number can be equal nine digits.");

            if (errors.Count > 0)
                throw new RgValidationException(errors);
        }
    }
}
