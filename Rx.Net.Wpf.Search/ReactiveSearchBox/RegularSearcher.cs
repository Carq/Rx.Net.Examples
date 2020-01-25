using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Rx.Net.Wpf.Search.ReactiveSearchBox
{
    public class RegularSearcher
    {
        private readonly TextBox _textBox;

        private readonly ListView _listView;

        private readonly IList<string> _cities;

        private ObservableCollection<string> _filteredList;

        public RegularSearcher(TextBox textBox, ListView listView, IList<string> cities)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _listView = listView ?? throw new ArgumentNullException(nameof(listView));
            _cities = cities ?? throw new ArgumentNullException(nameof(cities));
            MinimumLengthSearch();
            Init();
        }

        private void MinimumLengthSearch()
        {
            _textBox.TextChanged += TextChanged;
        }

        private void Init()
        {
            _filteredList = new ObservableCollection<string>(_cities.Take(100));
            _listView.ItemsSource = _filteredList;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchPhase = ((TextBox)sender).Text;
            if (string.IsNullOrWhiteSpace(searchPhase))
            {
                return;
            }

            if (searchPhase.Length <= 2)
            {
                return;
            }

            _filteredList.Clear();
            foreach (var item in _cities.Where(x => x.Contains(searchPhase, StringComparison.OrdinalIgnoreCase)))
            {
                _filteredList.Add(item);
            }
        }
    }
}
