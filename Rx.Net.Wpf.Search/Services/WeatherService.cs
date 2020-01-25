using Rx.Net.Wpf.Search.Services.DataPackages;
using Rx.Net.Wpf.Search.Services.ExternalApiDto;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.Net.Wpf.Search.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly CityFromJson[] _availableCities;

        private readonly string _apiKey = ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];

        public WeatherService()
        {
            var jsonString = File.ReadAllText("Data/city.list.json");
            _availableCities = JsonSerializer.Deserialize<CityFromJson[]>(jsonString);
        }

        public async Task<WeatherAvailability> IsWeatherAvailableAsync(string cityName)
        {
            await Task.Delay(100);
            if (_availableCities.Any(x => string.Equals(x.Name, cityName, System.StringComparison.OrdinalIgnoreCase)))
            {
                return WeatherAvailability.Available;
            }

            return WeatherAvailability.NotAvailable;
        }

        public async Task<WeatherInfo> GetWeatherAsync(string cityName)
        {
            var cityId = _availableCities.First(x => string.Equals(x.Name, cityName, System.StringComparison.OrdinalIgnoreCase)).Id;
            var response = await _httpClient.GetAsync($"https://openweathermap.org/data/2.5/weather?id={cityId}&appid={_apiKey}");
            var info = JsonSerializer.Deserialize<WeatherInfoJson>(await response.Content.ReadAsStringAsync());
            return new WeatherInfo(info.Temperature, info.IconName);
        }
    }
}
