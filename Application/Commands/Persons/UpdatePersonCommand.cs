namespace Application.Commands.Persons
{
    public sealed class UpdatePersonCommand
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public UpdatePersonCommand(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
