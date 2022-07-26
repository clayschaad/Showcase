using Showcase.Domain.Measurements.Temperatures;
using System.Text.Json;

namespace Showcase.Infrastructure.Persistence.FileStorage
{
    public class TemperaturePersistance : ITemperaturePersistance
    {
        private static readonly string storageFile = Path.GetTempFileName();

        public async Task<Temperature?> GetTemperatureAsync(Guid id, CancellationToken cancellation)
        {
            var temperatureList = await LoadAsnyc(storageFile);
            return temperatureList.SingleOrDefault(t => t.Id == id);
        }

        public async Task SaveTemperatureAsync(Temperature temperature, CancellationToken cancellation)
        {
            var temperatureList = await LoadAsnyc(storageFile);
            var savedTemperature = temperatureList.SingleOrDefault(t => t.Id == temperature.Id);
            if (savedTemperature == null)
            {
                temperatureList.Add(temperature);
            }
            else
            {

                savedTemperature.Value = temperature.Value;
                savedTemperature.Timestamp = temperature.Timestamp;
            }

            await SaveAsnyc(temperatureList, storageFile);
        }

        public async Task<IReadOnlyList<Temperature>> LoadTemperaturesAsync(CancellationToken cancellationToken)
        {
            var temperatures = await LoadAsnyc(storageFile);
            return temperatures.ToList();
        }

        private static async Task SaveAsnyc(IList<Temperature> temperatureList, string fileName)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(temperatureList, options);
            await File.WriteAllTextAsync(fileName, jsonString);
        }

        private static async Task<IList<Temperature>> LoadAsnyc(string fileName)
        {
            var jsonString = await File.ReadAllTextAsync(fileName);
            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                return JsonSerializer.Deserialize<IList<Temperature>>(jsonString);
            }

            return new List<Temperature>(); 
        }
    }
}
