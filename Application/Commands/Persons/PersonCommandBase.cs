using Application.Common.Commands.Documents;

namespace Application.Commands.Persons
{
    public abstract class PersonCommandBase
    {
        public string Name { get; private set; }
        public CpfCommand Cpf { get; private set; }
        public RgCommand Rg { get; private set; }

        protected PersonCommandBase(string name, CpfCommand cpf, RgCommand rg)
        {
            Name = name;
            Cpf = cpf;
            Rg = rg;
        }
    }
}
