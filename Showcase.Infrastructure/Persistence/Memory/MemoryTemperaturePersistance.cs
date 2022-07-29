using Showcase.Measurement.Domain.Weather;

namespace Showcase.Infrastructure.Persistence.Memory
{
    public class MemoryTemperaturePersistance : IWeatherMeasurementPersistance
    {
        private static Dictionary<Guid,Temperature> temperatureStorage = new Dictionary<Guid, Temperature>();
        private static Dictionary<Guid, Pressure> pressureStorage = new Dictionary<Guid, Pressure>();

        public async Task<Temperature?> GetTemperatureAsync(Guid id, CancellationToken cancellation)
        {
            return await Task.Run(() =>
            {

                if (temperatureStorage.ContainsKey(id))
                {
                    return temperatureStorage[id];
                }

                return null;
            });
        }

        public async Task SaveTemperatureAsync(Temperature temperature, CancellationToken cancellation)
        {
            await Task.Run(() =>
            {
                if (temperatureStorage.ContainsKey(temperature.Id))
                {
                    temperatureStorage[temperature.Id] = temperature;
                }
                else
                {
                    temperatureStorage.Add(temperature.Id, temperature);
                }
            });
        }

        public async Task<IReadOnlyList<Temperature>> LoadTemperaturesAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return temperatureStorage.Values.ToList();
            });
        }

        public async Task<Pressure?> GetPressureAsync(Guid id, CancellationToken cancellation)
        {
            return await Task.Run(() =>
            {

                if (pressureStorage.ContainsKey(id))
                {
                    return pressureStorage[id];
                }

                return null;
            });
        }

        public async Task SavePressureAsync(Pressure pressure, CancellationToken cancellation)
        {
            await Task.Run(() =>
            {
                if (pressureStorage.ContainsKey(pressure.Id))
                {
                    pressureStorage[pressure.Id] = pressure;
                }
                else
                {
                    pressureStorage.Add(pressure.Id, pressure);
                }
            });
        }

        public async Task<IReadOnlyList<Pressure>> LoadPressuresAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return pressureStorage.Values.ToList();
            });
        }
    }
}
