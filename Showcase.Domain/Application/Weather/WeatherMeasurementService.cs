using Showcase.Measurement.Domain;
using Showcase.Measurement.Domain.Weather;
using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Measurement.Application.Weather
{
    public class WeatherMeasurementService : IWeatherMeasurementService
    {
        private readonly IWeatherMeasurement weatherMeasurementService;
        private readonly IMeasurementSender measurementSender;
        private readonly IWeatherMeasurementPersistance weatherMeasurementPersistance;

        public WeatherMeasurementService(IWeatherMeasurement weatherMeasurementService, IMeasurementSender measurementSender, IWeatherMeasurementPersistance weatherMeasurementPersistance)
        {
            this.weatherMeasurementService = weatherMeasurementService;
            this.weatherMeasurementPersistance = weatherMeasurementPersistance;
            this.measurementSender = measurementSender;
        }

        public async Task MeasureWeatherAsync(double latitude, double longitude, CancellationToken cancellationToken)
        {
            var weatherRecord = await weatherMeasurementService.GetWeatherMeasurementAsync(latitude: latitude, longitude: longitude, cancellationToken);
            await measurementSender.SendMeasurement(weatherRecord, cancellationToken);
        }

        public async Task SaveWeatherAsync(WeatherRecord weatherRecord, CancellationToken cancellationToken)
        {
            var location = await weatherMeasurementPersistance.GetLocationAsync(latitued: weatherRecord.Latitude, longitued: weatherRecord.Longitude, cancellationToken);
            if (location == null)
            {
                location = Location.New(latitude: weatherRecord.Latitude, longitude: weatherRecord.Longitude);
            }

            weatherMeasurementPersistance.Add(Temperature.New(weatherRecord.Timestamp, weatherRecord.Temperature, location));
            weatherMeasurementPersistance.Add(Pressure.New(weatherRecord.Timestamp, weatherRecord.Pressure, location));

            await weatherMeasurementPersistance.SaveChangesAsync(cancellationToken);
        }
    }
}
