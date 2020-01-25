using System.IO;
using System.Threading.Tasks;

namespace Rx.Net.Wpf.Search.ReactiveService
{
    class CityReactiveServices
    {
        public async Task<string[]> SearchCitiesAsync()
        {
            return await File.ReadAllLinesAsync("PolishCities.txt");
        }
    }
}
