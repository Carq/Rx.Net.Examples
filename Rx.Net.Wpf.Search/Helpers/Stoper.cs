using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace Rx.Net.Wpf.Search.Helpers
{
    public class Stoper : IDisposable
    {
        private readonly Label _label;

        private readonly Stopwatch _watch;

        public Stoper(Label label)
        {
            _label = label;
            _label.Content = $"In progress...";
            _watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _watch.Stop();
            _label.Content = $"{_watch.ElapsedMilliseconds}ms - {_watch.ElapsedMilliseconds / 1000.0m:0.##} s";
        }
    }
}
