using Shared.Enums;

namespace Application.Features.Persons.Commands
{
    public abstract class PersonCommandBase
    {
        public string Name { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
        public string CpfNumber { get; private set; }
        public DateTimeOffset CpfRegistrationDate { get; private set; }
        public string RgNumber { get; private set; }
        public InssuingAuthority RgIssuingAuthority { get; private set; }

        protected PersonCommandBase(
            string name,
            DateTimeOffset birthDate,
            string cpfNumber,
            DateTimeOffset cpfRegistrationDate,
            string rgNumber,
            InssuingAuthority rgIssuingAuthority)
        {
            Name = name;
            BirthDate = birthDate;
            CpfNumber = cpfNumber;
            CpfRegistrationDate = cpfRegistrationDate;
            RgNumber = rgNumber;
            RgIssuingAuthority = rgIssuingAuthority;
        }
    }
}
