namespace Application.Common.Response.Documents
{
    public class DocumentResponse
    {
        public string Number { get; private set; }
        public DateTime BirthDate { get; private set; }

        protected DocumentResponse(string number, DateTime birthDate)
        {
            Number = number;
            BirthDate = birthDate;
        }
    }
}
