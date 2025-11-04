namespace Application.Common.Commands.Documents
{
    public class CpfCommand : DocumentCommand
    {
        public DateTime RegistrationDate { get; private set; }

        public CpfCommand(string number, DateTime birthDate, DateTime registrationDate) : base(number, birthDate)
        {
            RegistrationDate = registrationDate;
        }
    }
}
