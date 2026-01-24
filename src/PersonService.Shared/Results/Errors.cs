namespace Shared.Results
{
    public static class Errors
    {
        public static class Person
        {
            public static Error DuplicateName => new("Person.Conflict", "There is already a person with that name.", ErrorType.Conflict);
            public static Error NotFound => new("Person.NotFound", "Person was not found.", ErrorType.NotFound);
        }

        public static class Cpf
        {
            public static Error DuplicateNumber => new("Cpf.Conflict", "There is already a CPF with that number.", ErrorType.Conflict);
        }
        
        public static class Rg
        {
            public static Error DuplicateNumber => new("Rg.Conflict", "There is already a RG with that number.", ErrorType.Conflict);
        }
    }
}
