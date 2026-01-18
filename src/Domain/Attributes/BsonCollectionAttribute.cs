namespace Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class BsonCollectionAttribute : Attribute
    {
        public string Name { get; }

        public BsonCollectionAttribute(string name)
        {
            Name = name;
        }
    }
}
