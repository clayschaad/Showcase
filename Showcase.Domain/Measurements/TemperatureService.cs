using Showcase.Domain.Measurements.Temperatures;

namespace Showcase.Domain.Measurements
{
    public class TemperatureService : ITemperatureService
    {
        private readonly ITemperatureMeasurement temperatureMeasurement;
        private readonly ITemperaturePersistance temperaturePersistance;
        private readonly ITemperatureSending temperatureSending;

        public TemperatureService(ITemperatureMeasurement temperatureMeasurement, ITemperaturePersistance temperaturePersistance, ITemperatureSending temperatureSending)
        {
            this.temperatureMeasurement = temperatureMeasurement;
            this.temperaturePersistance = temperaturePersistance;
            this.temperatureSending = temperatureSending;
        }

        public async Task<IReadOnlyList<Temperature>> GetTemperatures(CancellationToken cancellationToken)
        {
            return await temperaturePersistance.LoadTemperaturesAsync(cancellationToken);
        }

        public async Task<double> MeasureTemperatureAsync(Coordinates coordinates, CancellationToken cancellationToken)
        {
            var temperatureValue = await temperatureMeasurement.GetTemperatureAsync(coordinates, cancellationToken);
            var temperature = Temperature.NewMeasurement(temperatureValue, DateTime.UtcNow, coordinates);

            await temperatureSending.SendTemperatureAsync(temperature, cancellationToken);

            await temperaturePersistance.SaveTemperatureAsync(temperature, cancellationToken);
            return temperature.Value;
        }
    }
}
