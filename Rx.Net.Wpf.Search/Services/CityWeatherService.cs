using Rx.Net.Wpf.Search.Services.DataPackages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rx.Net.Wpf.Search.Services
{
    public class CityWeatherService
    {
        private const int MaxRequestsForWeather = Consts.MaxRequestsPerSearch;

        private readonly CityService _cityService = new CityService();

        private readonly WeatherService _weatherService = new WeatherService();

        public async Task<IList<CityWithWeatherInfo>> SearchCityAndLoadWeatherInfo(string searchPhase)
        {
            var cities = await _cityService.SearchCitiesAsync(searchPhase);
            var citiesWithWeather = new List<CityWithWeatherInfo>();
            int requestsMade = 0;

            foreach (var city in cities)
            {
                var weatherAvailability = await _weatherService.IsWeatherAvailableAsync(city);
                if (weatherAvailability == WeatherAvailability.Available)
                {
                    if (requestsMade++ < MaxRequestsForWeather )
                    {
                        var info = await _weatherService.GetWeatherAsync(city);
                        citiesWithWeather.Add(new CityWithWeatherInfo(city, info));
                    }
                    else 
                    {
                        citiesWithWeather.Add(new CityWithWeatherInfo(city, WeatherAvailability.TemporaryNotAvailable));
                    }
                }
                else
                {
                    citiesWithWeather.Add(new CityWithWeatherInfo(city, WeatherAvailability.NotAvailable));
                }
            }

            return citiesWithWeather;
        }
    }
}
