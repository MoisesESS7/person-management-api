using Shared.Enums;

namespace Application.Features.Persons.Responses
{
    public sealed class PersonResponse : ResponseBase
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
        public string CpfNumber { get; private set; }
        public DateTimeOffset CpfRegistrationDate { get; private set; }
        public string RgNumber { get; private set; }
        public InssuingAuthority RgIssuingAuthority { get; private set; }

        public PersonResponse(
            string id,
            string name,
            int age,
            DateTimeOffset birthDate,
            string cpfNumber,
            DateTimeOffset cpfRegistrationDate,
            string rgNumber,
            InssuingAuthority rgIssuingAuthority,
            DateTimeOffset createdAt,
            DateTimeOffset? updatedAt,
            DateTimeOffset? deletedAt)
            : base (id, createdAt, updatedAt, deletedAt)
        {
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
