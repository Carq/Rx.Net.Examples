using System.Text.Json.Serialization;

namespace Rx.Net.Wpf.Search.Services.ExternalApiDto
{
    public class CityFromJson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
