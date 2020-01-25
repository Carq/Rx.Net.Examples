using MahApps.Metro.Controls;
using Rx.Net.Wpf.Search.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rx.Net.Wpf.Search.ReactiveSearchBox
{
    public partial class ReactiveSearchBoxControl : MetroContentControl
    {
        private CityService _cityServies = new CityService();

        private RegularSearcher regularSearcher;

        private ReactiveSearcher reactiveSearcher;

        public ReactiveSearchBoxControl()
        {
            InitializeComponent();
            Loaded += ControlOnLoaded;
        }

        public ObservableCollection<string> RegularCities { get; private set; } = new ObservableCollection<string>();

        public IList<string> ReactiveCities { get; private set; }

        private async void ControlOnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var polishCities = await _cityServies.LoadCitiesAsync();
            regularSearcher = new RegularSearcher(RegularTextBox, ReguralListView, polishCities);
            reactiveSearcher = new ReactiveSearcher(ReactiveTextBox, ReactiveListView, polishCities);
        }
    }
}
