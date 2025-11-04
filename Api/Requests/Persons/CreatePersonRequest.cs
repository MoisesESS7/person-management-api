using Shared.Enums;

namespace Api.Requests.Persons
{
    public class CreatePersonRequest
    {
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CpfNumber { get; private set; }
        public DateTime CpfRegistrationDate { get; private set; }
        public string RgNumber { get; private set; }
        public InssuingAuthority RgIssuingAuthority { get; private set; }

        public CreatePersonRequest(
            string name,
            DateTime birthDate,
            string cpfNumber,
            DateTime cpfRegistrationDate,
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
