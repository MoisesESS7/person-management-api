using Application.Common.Response.Documents;

namespace Application.Common.Response.Persons
{
    public class PersonResponse
    {
        public string Id { get; }
        public string Name { get; private set; }
        public int Age { get; }
        public CpfResponse Cpf { get; }
        public RgResponse Rg { get; }

        public PersonResponse(string id, string name, int age, CpfResponse cpf, RgResponse rg)
        {
            Id = id;
            Name = name;
            Age = age;
            Cpf = cpf;
            Rg = rg;
        }
    }
}
