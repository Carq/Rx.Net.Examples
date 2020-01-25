using Rx.Net.Wpf.Search.Services.DataPackages;
using System;
using System.Reactive.Linq;

namespace Rx.Net.Wpf.Search.RxServices
{
    public class CityWeatherRxService
    {
        private readonly CityRxServices _cityRxServices = new CityRxServices();

        private readonly WeatherRxService _weatherRxService = new WeatherRxService();

        public IObservable<CityWithWeatherInfo> SearchCityAndLoadWeatherInfo(string searchPhase)
        {
            return _cityRxServices
              .SearchCities(searchPhase)
                .Select(city => _weatherRxService.LoadWeatherInfoRx(city))
                //.Select(city => cityWeatherRxService.LoadWeatherInfoAsync(city).ToObservable())
                .Merge();
        }
    }
}
