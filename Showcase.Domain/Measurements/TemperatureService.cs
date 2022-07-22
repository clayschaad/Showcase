namespace Showcase.Domain.Measurements
{
    public class TemperatureService : ITemperatureService
    {
        private readonly ITemperatureMeasurement temperatureMeasurement;
        private readonly ITemperaturePersistance temperaturePersistance;

        public TemperatureService(ITemperatureMeasurement temperatureMeasurement, ITemperaturePersistance temperaturePersistance)
        {
            this.temperatureMeasurement = temperatureMeasurement;
            this.temperaturePersistance = temperaturePersistance;
        }

        public async Task<Temperature> MeasureTemperatureAsync(CancellationToken cancellationToken)
        {
            var temperatureValue = await temperatureMeasurement.GetTemperatureAsync(cancellationToken);
            var temperature = Temperature.NewMeasurement(temperatureValue, DateTime.UtcNow);
            await temperaturePersistance.SaveTemperatureAsync(temperature, cancellationToken);
            return temperature;
        }
    }
}
