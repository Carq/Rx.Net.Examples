using System;

namespace Rx.Net.Wpf.Search.Services.DataPackages
{
    public class CityWithWeatherInfo
    {
        public CityWithWeatherInfo(string city, WeatherInfo weatherInfo)
        {
            City = city ?? throw new ArgumentNullException(nameof(city));
            Info = weatherInfo;
            Status = WeatherAvailability.Available;
        }

        public CityWithWeatherInfo(string city, WeatherAvailability weatherAvailability)
        {
            City = city ?? throw new ArgumentNullException(nameof(city));

            if (weatherAvailability == WeatherAvailability.Available)
            {
                throw new ArgumentException("Cannot set Available status without specified temperature");
            }

            Status = weatherAvailability;
        }

        public string City { get; }

        public float? Temperature => Info?.Temperature;

        public string Icon => Info?.Icon;

        public WeatherInfo Info { get; }

        public WeatherAvailability Status { get; }
    }
}
