using Microsoft.Extensions.Configuration;
using Showcase.Measurement.Domain;
using Showcase.Measurement.Domain.Weather;
using Showcase.Measurement.Domain.Weather.Aggregate;
using System.Net.Http.Json;

namespace Showcase.Infrastructure.Measurement.OpenWeatherMap
{
    public class OpenWeatherMapMeasurement : IWeatherMeasurement
    {
        private readonly IConfiguration configuration;

        public OpenWeatherMapMeasurement(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<WeatherRecord> GetWeatherMeasurementAsync(Coordinates coordinates, CancellationToken cancellation)
        {
            var httpClient = new HttpClient();
            var options = configuration.GetSection(MeasurementOptions.SectionKey).Get<MeasurementOptions>();

            var weatherMeasurement = await httpClient.GetFromJsonAsync<WeatherMeasurement>($"https://api.openweathermap.org/data/2.5/weather?lat={coordinates.Latitude}&lon={coordinates.Longitude}&units=metric&appid={options.OpenWeatherMapApiKey}");
            if (weatherMeasurement == null)
            {
                throw new MeasurementException("Cannot parse measurement result");
            }

            return new WeatherRecord(Temperature: weatherMeasurement.Main.Temp, Pressure: weatherMeasurement.Main.Pressure);
        }
    }
}
