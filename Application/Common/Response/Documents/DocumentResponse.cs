namespace Application.Common.Response.Documents
{
    public class DocumentResponse
    {
        public string Number { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }

        protected DocumentResponse(string number, DateTimeOffset birthDate)
        {
            Number = number;
            BirthDate = birthDate;
        }
    }
}
