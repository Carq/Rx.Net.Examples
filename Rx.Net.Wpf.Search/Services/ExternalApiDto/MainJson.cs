using System.Text.Json.Serialization;

namespace Rx.Net.Wpf.Search.Services.ExternalApiDto
{
    public class MainJson
    {
        [JsonPropertyName("temp")]
        public float Temperature { get; set; }
    }
}
