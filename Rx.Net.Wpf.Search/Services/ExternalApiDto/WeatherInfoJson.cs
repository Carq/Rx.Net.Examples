using System.Linq;
using System.Text.Json.Serialization;

namespace Rx.Net.Wpf.Search.Services.ExternalApiDto
{
    public class WeatherInfoJson
    {
        [JsonPropertyName("weather")]
        public WeatherJson[] Weathers { get; set; }

        [JsonPropertyName("main")]
        public MainJson Main { get; set; }

        public string IconName => Weathers.First().IconName;

        public float Temperature => Main.Temperature;
    }
}
