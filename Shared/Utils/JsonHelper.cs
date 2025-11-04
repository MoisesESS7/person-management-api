using System.Text.Json;

namespace Shared.Utils
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions  _readOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        private static readonly JsonSerializerOptions  _writeOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static string Serialize<T>(T input)
        {
            return JsonSerializer.Serialize(input, _writeOptions);
        }
        
        public static T? Deserialize<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input, _readOptions);
        }
    }
}
