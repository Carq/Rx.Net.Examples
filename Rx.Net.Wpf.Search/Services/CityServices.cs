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
            if (string.IsNullOrEmpty(searchPhase))
            {
                return await LoadCitiesAsync();
            }

            return (await File.ReadAllLinesAsync("Data/PolishCities.txt")).Where(x => x.Contains(searchPhase, StringComparison.OrdinalIgnoreCase)).ToArray();
        }
    }
}
