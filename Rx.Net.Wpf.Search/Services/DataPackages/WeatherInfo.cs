namespace Rx.Net.Wpf.Search.Services.DataPackages
{
    public class WeatherInfo
    {
        public WeatherInfo(float temperature, string iconName)
        {
            Temperature = temperature;
            Icon = $"http://openweathermap.org/img/wn/{iconName}.png";
        }

        public float Temperature { get; }

        public string Icon { get; }
    }
}
