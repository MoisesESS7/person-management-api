namespace Application.Common.Commands.Documents
{
    public class DocumentCommand
    {
        public string Number { get; private set; }
        public DateTime BirthDate { get; private set; }

        protected DocumentCommand(string number, DateTime birthDate)
        {
            Number = number;
            BirthDate = birthDate;
        }
    }
}
