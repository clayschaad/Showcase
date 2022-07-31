using Microsoft.Extensions.Configuration;
using Showcase.Measurement.Domain;
using Showcase.Measurement.Domain.Weather;
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

        public async Task<WeatherRecord> GetWeatherMeasurementAsync(double latitude, double longitude, CancellationToken cancellation)
        {
            var httpClient = new HttpClient();
            var options = configuration.GetSection(MeasurementOptions.SectionKey).Get<MeasurementOptions>();

            var weatherMeasurement = await httpClient.GetFromJsonAsync<WeatherMeasurement>($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid={options.OpenWeatherMapApiKey}");
            if (weatherMeasurement == null)
            {
                throw new MeasurementException("Cannot parse measurement result");
            }

            return new WeatherRecord(Timestamp: DateTime.UtcNow, Latitude: latitude, Longitude: longitude, Temperature: weatherMeasurement.Main.Temp, Pressure: weatherMeasurement.Main.Pressure);
        }
    }
}
