using Application.Common.Commands.Documents;

namespace Application.Commands.Persons
{
    public sealed class CreatePersonCommand : PersonCommandBase
    {
        public CreatePersonCommand(string name, CpfCommand cpf, RgCommand rg) : base(name, cpf, rg)
        {
        }
    }
}
