using PersonService.Domain.Exceptions.Documents;

namespace PersonService.Domain.ValueObjects.Bases
{
    public abstract class Document
    {
        public string Number { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }

        protected Document(string number, DateTimeOffset birthDate)
        {
            Number = number;
            BirthDate = birthDate;
            Validation();
        }

        private void Validation()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Number))
                errors.Add("The document number cannot be empty.");

            if (BirthDate == default)
                errors.Add("The birth date can be informed.");

            if (BirthDate.Date > DateTimeOffset.Now.Date)
                errors.Add("The birth date cannot has in future.");

            if(errors.Count > 0)
                throw new DocumentValidationException(errors);
        }
    }
}
