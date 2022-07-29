using Microsoft.Extensions.Configuration;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Weather;
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

        public async Task<Domain.Measurements.Weather.WeatherMeasurement> GetWeatherMeasurementAsync(Coordinates coordinates, CancellationToken cancellation)
        {
            var httpClient = new HttpClient();
            var options = configuration.GetSection(MeasurementOptions.SectionKey).Get<MeasurementOptions>();

            var weatherMeasurement = await httpClient.GetFromJsonAsync<WeatherMeasurement>($"https://api.openweathermap.org/data/2.5/weather?lat={coordinates.Latitude}&lon={coordinates.Longitude}&units=metric&appid={options.OpenWeatherMapApiKey}");
            if (weatherMeasurement == null)
            {
                throw new MeasurementException("Cannot parse measurement result");
            }

            return new Domain.Measurements.Weather.WeatherMeasurement(Temperature: weatherMeasurement.Main.Temp, Pressure: weatherMeasurement.Main.Pressure);
        }
    }
}
