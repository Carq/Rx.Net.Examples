using Rx.Net.Wpf.Search.Services;
using Rx.Net.Wpf.Search.Services.DataPackages;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Rx.Net.Wpf.Search.RxServices
{
    public class WeatherRxService
    {
        private readonly WeatherService _weatherService = new WeatherService();

        private const int MaxRequestsForWeather = Consts.MaxRequestsPerSearch;

        private int _requests = 0;

        public IObservable<CityWithWeatherInfo> LoadWeatherInfoRx(string city)
        {
            return Observable.Create<CityWithWeatherInfo>(async observer =>
            {
                var weatherAvailability = await _weatherService.IsWeatherAvailableAsync(city);
                if (weatherAvailability == WeatherAvailability.Available)
                {
                    if (_requests++ < MaxRequestsForWeather)
                    {
                        var info = await _weatherService.GetWeatherAsync(city);
                        observer.OnNext(new CityWithWeatherInfo(city, info));
                    }
                    else
                    {
                        observer.OnNext(new CityWithWeatherInfo(city, WeatherAvailability.TemporaryNotAvailable));
                    }
                }
                else
                {
                    observer.OnNext(new CityWithWeatherInfo(city, WeatherAvailability.NotAvailable));
                }
            });
        }

        public IObservable<CityWithWeatherInfo> LoadWeatherInfoRxAsync(string city)
        {
            return Observable.FromAsync(async observer =>
            {
                var weatherAvailability = await _weatherService.IsWeatherAvailableAsync(city);
                if (weatherAvailability == WeatherAvailability.Available)
                {
                    if (_requests++ < MaxRequestsForWeather)
                    {
                        var info = await _weatherService.GetWeatherAsync(city);
                        return new CityWithWeatherInfo(city, info);
                    }
                    else
                    {
                        return new CityWithWeatherInfo(city, WeatherAvailability.TemporaryNotAvailable);
                    }
                }
                else
                {
                    return new CityWithWeatherInfo(city, WeatherAvailability.NotAvailable);
                }
            });
        }

        public async Task<CityWithWeatherInfo> LoadWeatherInfoAsync(string city)
        {
            var weatherAvailability = await _weatherService.IsWeatherAvailableAsync(city);
            if (weatherAvailability == WeatherAvailability.Available)
            {
                var info = await _weatherService.GetWeatherAsync(city);
                return new CityWithWeatherInfo(city, info);
            }
            else
            {
                return new CityWithWeatherInfo(city, WeatherAvailability.NotAvailable);
            }
        }
    }
}
