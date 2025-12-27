using Domain.Exceptions.Documents;
using Domain.ValueObjects.Bases;
using Shared.Enums;

namespace Domain.ValueObjects
{
    public sealed class Rg : Document
    {
        public InssuingAuthority IssuingAuthority { get; private set; }

        public Rg(string number, DateTimeOffset birthDate, InssuingAuthority issuingAuthority) : base(number, birthDate)
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
