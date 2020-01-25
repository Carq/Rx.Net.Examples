using System;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Rx.Net.Wpf.Search.RxServices
{
    class CityRxServices
    {
        public CityRxServices()
        {
        }

        public IObservable<string> LoadCities()
        {
            return Observable.Create<string>(async observer =>
            {
                using (var fileStream = File.OpenRead("Data/PolishCities.txt"))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string line;
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        observer.OnNext(line);
                    }

                    observer.OnCompleted();
                }
            });
        }

        public IObservable<string> SearchCities(string searchPhase)
        {
            if (string.IsNullOrEmpty(searchPhase))
            {
                return LoadCities();
            }

            return Observable.Create<string>(async observer =>
            {
                using (var fileStream = File.OpenRead("Data/PolishCities.txt"))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
                {
                    string line;
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        if (line.Contains(searchPhase, StringComparison.OrdinalIgnoreCase))
                        {
                            observer.OnNext(line);
                        }
                    }

                    observer.OnCompleted();
                }

                return Disposable.Empty;
            });
        }
    }
}
