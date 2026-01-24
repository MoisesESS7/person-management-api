using PersonService.Shared.Enums;

namespace PersonService.Api.Contracts.Responses.Persons
{
    public class PersonResponse
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }
        public int Age { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
        public string CpfNumber { get; private set; }
        public DateTimeOffset CpfRegistrationDate { get; private set; }
        public string RgNumber { get; private set; }
        public IssuingAuthority RgIssuingAuthority { get; private set; }

        public PersonResponse(
            string id,
            string name,
            DateTimeOffset createdAt,
            DateTimeOffset? updatedAt,
            DateTimeOffset? deletedAt,
            int age,
            DateTimeOffset birthDate,
            string cpfNumber,
            DateTimeOffset cpfRegistrationDate,
            string rgNumber,
            IssuingAuthority rgIssuingAuthority)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            DeletedAt = deletedAt;
            Age = age;
            BirthDate = birthDate;
            CpfNumber = cpfNumber;
            CpfRegistrationDate = cpfRegistrationDate;
            RgNumber = rgNumber;
            RgIssuingAuthority = rgIssuingAuthority;
        }
    }
}
