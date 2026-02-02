using PersonService.Application.Features.Persons.Commands.Update;

namespace PersonService.Tests.Unit.Application.Features.Persons.Commands.Update
{
    public sealed class UpdatePersonCommandBuilder
    {
        private string? _id = Guid.NewGuid().ToString();
        private string? _name = "John Doe";

        public UpdatePersonCommandBuilder WithId(string? id)
        {
            _id = id;
            return this;
        }

        public UpdatePersonCommandBuilder WithName(string? name)
        {
            _name = name;
            return this;
        }

        public UpdatePersonCommand Build() => new(_id!, _name!);
    }
}
