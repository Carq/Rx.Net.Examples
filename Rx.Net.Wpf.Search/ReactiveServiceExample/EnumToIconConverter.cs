using Rx.Net.Wpf.Search.Services.DataPackages;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Rx.Net.Wpf.Search.ReactiveServiceExample
{
    public class EnumToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((WeatherAvailability)value) switch
            {
                WeatherAvailability.Available => "✔",
                WeatherAvailability.NotAvailable => "❌",
                WeatherAvailability.TemporaryNotAvailable => "⚠",
                _ => string.Empty,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
