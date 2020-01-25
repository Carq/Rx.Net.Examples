using System.Text.Json.Serialization;

namespace Rx.Net.Wpf.Search.Services.ExternalApiDto
{
    public class WeatherJson
    {
        [JsonPropertyName("icon")]
        public string IconName { get; set; }
    }
}
