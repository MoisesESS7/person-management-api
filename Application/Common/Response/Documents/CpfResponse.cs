namespace Application.Common.Response.Documents
{
    public class CpfResponse : DocumentResponse
    {
        public DateTimeOffset RegistrationDate { get; private set; }

        public CpfResponse(string number, DateTimeOffset birthDate, DateTimeOffset registrationDate) : base(number, birthDate)
        {
            RegistrationDate = registrationDate;
        }
    }
}
