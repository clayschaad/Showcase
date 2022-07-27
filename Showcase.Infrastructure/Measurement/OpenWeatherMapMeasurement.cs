using Microsoft.Extensions.Configuration;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Temperatures;
using System.Net.Http.Json;

namespace Showcase.Infrastructure.Measurement
{
    public class OpenWeatherMapMeasurement : ITemperatureMeasurement
    {
        private readonly IConfiguration configuration;

        public OpenWeatherMapMeasurement(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<double> GetTemperatureAsync(Coordinates coordinates, CancellationToken cancellation)
        {
            var httpClient = new HttpClient();
            var options = configuration.GetSection(MeasurementOptions.SectionKey).Get<MeasurementOptions>();

            var temperature = await httpClient.GetFromJsonAsync<WeatherMeasurement>($"https://api.openweathermap.org/data/2.5/weather?lat={coordinates.Latitude}&lon={coordinates.Longitude}&units=metric&appid={options.OpenWeatherMapApiKey}");
            if (temperature == null)
            {
                throw new MeasurementException("Cannot parse temperature measurement result");
            }

            return temperature.Main.Temp;
        }
    }
}
