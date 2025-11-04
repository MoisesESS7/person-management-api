namespace Application.Common.Response.Documents
{
    public class CpfResponse : DocumentResponse
    {
        public DateTime RegistrationDate { get; private set; }

        public CpfResponse(string number, DateTime birthDate, DateTime registrationDate) : base(number, birthDate)
        {
            RegistrationDate = registrationDate;
        }
    }
}
