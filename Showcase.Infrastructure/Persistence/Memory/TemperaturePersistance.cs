using Showcase.Domain.Measurements.Temperatures;

namespace Showcase.Infrastructure.Persistence.Memory
{
    public class TemperaturePersistance : ITemperaturePersistance
    {
        private static Dictionary<Guid,Temperature> temperatureStorage = new Dictionary<Guid, Temperature>();

        public async Task<Temperature?> GetTemperatureAsync(Guid id, CancellationToken cancellation)
        {
            if (temperatureStorage.ContainsKey(id))
            {
                return temperatureStorage[id];
            }

            return null;
        }

        public async Task SaveTemperatureAsync(Temperature temperature, CancellationToken cancellation)
        {
            if (temperatureStorage.ContainsKey(temperature.Id))
            {
                temperatureStorage[temperature.Id] = temperature;
            }
            else
            {
                temperatureStorage.Add(temperature.Id, temperature);    
            }
        }
    }
}
