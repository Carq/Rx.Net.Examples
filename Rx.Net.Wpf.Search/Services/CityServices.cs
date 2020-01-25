using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rx.Net.Wpf.Search.Services
{
    public class CityService
    {
        public async Task<string[]> LoadCitiesAsync()
        {
            return await File.ReadAllLinesAsync("Data/PolishCities.txt");
        }

        public async Task<string[]> SearchCitiesAsync(string searchPhase)
        {
            return (await File.ReadAllLinesAsync("Data/PolishCities.txt")).Where(x => x.Contains(searchPhase, StringComparison.OrdinalIgnoreCase)).ToArray();
        }
    }
}
