﻿using Showcase.Domain.Measurements.Weather;

namespace Showcase.Domain.Measurements
{
    public class WeatherMeasurementService : IWeatherMeasurementService
    {
        private readonly IWeatherMeasurement weatherMeasurement;
        private readonly IWeatherMeasurementPersistance weatherMeasurementPersistance;
        private readonly IWeatherMeasurementSender weatherMeasurementSender;

        public WeatherMeasurementService(IWeatherMeasurement weatherMeasurement, IWeatherMeasurementPersistance weatherMeasurementPersistance, IWeatherMeasurementSender weatherMeasurementSender)
        {
            this.weatherMeasurement = weatherMeasurement;
            this.weatherMeasurementPersistance = weatherMeasurementPersistance;
            this.weatherMeasurementSender = weatherMeasurementSender;
        }

        public async Task<IReadOnlyList<Temperature>> GetTemperaturesAsync(CancellationToken cancellationToken)
        {
            return await weatherMeasurementPersistance.LoadTemperaturesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Pressure>> GetPressuresAsync(CancellationToken cancellationToken)
        {
            return await weatherMeasurementPersistance.LoadPressuresAsync(cancellationToken);
        }

        public async Task MeasureWeatherAsync(Coordinates coordinates, CancellationToken cancellationToken)
        {
            var weatherMeasurementResult = await weatherMeasurement.GetWeatherMeasurementAsync(coordinates, cancellationToken);

            var temperature = Temperature.NewMeasurement(weatherMeasurementResult.Temperature, DateTime.UtcNow, coordinates);
            await weatherMeasurementSender.SendWeatherMeasurement(temperature, cancellationToken);

            var pressure = Pressure.NewMeasurement(weatherMeasurementResult.Pressure, DateTime.UtcNow, coordinates);
            await weatherMeasurementSender.SendWeatherMeasurement(pressure, cancellationToken);
        }
    }
}
