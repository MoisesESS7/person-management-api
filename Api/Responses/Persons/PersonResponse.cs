using Shared.Enums;

namespace Api.Responses.Persons
{
    public class PersonResponse
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CpfNumber { get; private set; }
        public DateTime CpfRegistrationDate { get; private set; }
        public string RgNumber { get; private set; }
        public InssuingAuthority RgIssuingAuthority { get; private set; }

        public PersonResponse(
            string id,
            string name,
            int age,
            DateTime birthDate,
            string cpfNumber,
            DateTime cpfRegistrationDate,
            string rgNumber,
            InssuingAuthority rgIssuingAuthority)
        {
            Id = id;
            Name = name;
            Age = age;
            BirthDate = birthDate;
            CpfNumber = cpfNumber;
            CpfRegistrationDate = cpfRegistrationDate;
            RgNumber = rgNumber;
            RgIssuingAuthority = rgIssuingAuthority;
        }
    }
}
