using System.Text.Json.Serialization;

namespace Api.Contracts.Requests.Persons
{
    public class UpdatePersonRequest
    {
        [JsonIgnore]
        public string Id { get; private set; }
        public string Name { get; private set; }

        public UpdatePersonRequest(string name)
        {
            Id = string.Empty;
            Name = name;
        }

        public void SetId(string id)
        {
            Id = id ?? string.Empty;
        }
    }
}
