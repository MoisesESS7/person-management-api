using PersonService.Shared.Enums;

namespace PersonService.Api.Contracts.Requests.Persons
{
    public class CreatePersonRequest
    {
        public string Name { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
        public string CpfNumber { get; private set; }
        public DateTimeOffset CpfRegistrationDate { get; private set; }
        public string RgNumber { get; private set; }
        public IssuingAuthority RgIssuingAuthority { get; private set; }

        public CreatePersonRequest(
            string name,
            DateTimeOffset birthDate,
            string cpfNumber,
            DateTimeOffset cpfRegistrationDate,
            string rgNumber,
            IssuingAuthority rgIssuingAuthority)
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
