using Domain.Exceptions.Documents;

namespace Domain.ValueObjects.Bases
{
    public abstract class Document
    {
        public string Number { get; set; }
        public DateTime BirthDate { get; set; }

        protected Document(string number, DateTime birthDate)
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

            if (BirthDate.Date > DateTime.Now.Date)
                errors.Add("The birth date cannot has in future.");

            if(errors.Count > 0)
                throw new DocumentValidationException(errors);
        }
    }
}
