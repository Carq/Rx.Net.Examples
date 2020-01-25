using MahApps.Metro.Controls;
using Rx.Net.Wpf.Search.Services;
using Rx.Net.Wpf.Search.Services.DataPackages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Rx.Net.Wpf.Search.ReactiveServiceExample
{
    public partial class ReactiveServiceControl : MetroContentControl
    {
        private readonly CityWeatherService _cityWeatherService;

        public ReactiveServiceControl()
        {
            InitializeComponent();
            _cityWeatherService = new CityWeatherService();
            Init();
        }

        private void Init()
        {
            DataGrid.ItemsSource = CitiesWithWeather;
        }

        public ObservableCollection<CityWithWeatherInfo> CitiesWithWeather { get; private set; } = new ObservableCollection<CityWithWeatherInfo>();

        private void DisplayData(IList<CityWithWeatherInfo> cityWithWeathers)
        {
            CitiesWithWeather.Clear();
            foreach (var item in cityWithWeathers)
            {
                CitiesWithWeather.Add(item);
            }
        }

        private async void TextBoxOnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                return;
            }

            ProgressRing.IsActive = true;
            var result = await Dispatcher.Invoke(async () => await _cityWeatherService.SearchCityAndDisplayWeather(TextBox.Text));
            DisplayData(result);
            ProgressRing.IsActive = false;
           
        }
    }
}
