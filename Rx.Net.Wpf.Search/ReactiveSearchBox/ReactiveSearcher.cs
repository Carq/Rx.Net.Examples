using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Controls;

namespace Rx.Net.Wpf.Search.ReactiveSearchBox
{
    public class ReactiveSearcher
    {
        private readonly TextBox _textBox;

        private readonly ListView _listView;

        private readonly IList<string> _cities;

        private ObservableCollection<string> _filteredList;

        public ReactiveSearcher(TextBox textBox, ListView listView, IList<string> cities)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _listView = listView ?? throw new ArgumentNullException(nameof(listView));
            _cities = cities ?? throw new ArgumentNullException(nameof(cities));
            Init();

            var searchPhaseSource = CreateObservableString();
            WithDelaySearch(searchPhaseSource);
            // MinimumLengthSearch(searchPhaseSource);
        }

        private void WithDelaySearch(IObservable<string> searchPhaseSource)
        {
            searchPhaseSource
                .Throttle(TimeSpan.FromSeconds(2))
                .Where(x => x.Length > 2)
                .ObserveOnDispatcher()
                .Subscribe(x => PerformSearch(x));
        }

        private void MinimumLengthSearch(IObservable<string> searchPhaseSource)
        {
            searchPhaseSource
                .Where(x => x.Length > 2)
                .Subscribe(x => PerformSearch(x));
        }

        private void Init()
        {
            _filteredList = new ObservableCollection<string>(_cities.Take(100));
            _listView.ItemsSource = _filteredList;
        }

        private IObservable<string> CreateObservableString()
        {
            return Observable.FromEventPattern<TextChangedEventArgs>(_textBox, "TextChanged")
                .Select(e => ((TextBox)e.Sender).Text);
        }

        private void PerformSearch(string searchPhase)
        {
            _filteredList.Clear();
            foreach (var item in _cities.Where(x => x.Contains(searchPhase, StringComparison.OrdinalIgnoreCase)))
            {
                _filteredList.Add(item);
            }
        }
    }
}
