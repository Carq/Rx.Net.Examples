using MahApps.Metro.Controls;
using Rx.Net.Wpf.Search.Helpers;
using Rx.Net.Wpf.Search.RxServices;
using Rx.Net.Wpf.Search.Services;
using Rx.Net.Wpf.Search.Services.DataPackages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
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

        private void DisplayCount()
        {
            Count.Content = $"({CitiesWithWeather.Count})";
        }

        private async void ButtonNormalOnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
            CitiesWithWeather.Clear();
            DisplayCount();

            using (new Stoper(Time))
            {
                var result = await _cityWeatherService.SearchCityAndLoadWeatherInfo(TextBox.Text);
                DisplayData(result);
            }

            DisplayCount();
            ProgressRing.IsActive = false;
        }

        private void ButtonReacativeOnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var cityWeatherRxService = new CityWeatherRxService();
            
            CitiesWithWeather.Clear();
            DisplayCount();

            var stoper = new Stoper(Time);

            cityWeatherRxService
                .SearchCityAndLoadWeatherInfo(TextBox.Text)
                .Buffer(TimeSpan.FromSeconds(2))
                .ObserveOnDispatcher()
                .Subscribe(
                x =>
                {
                    foreach (var item in x)
                    {
                        CitiesWithWeather.Add(item);
                    }

                    DisplayCount();
                },
                () =>
                {
                    stoper.Dispose();
                    DisplayCount();
                });
        }
    }
}
